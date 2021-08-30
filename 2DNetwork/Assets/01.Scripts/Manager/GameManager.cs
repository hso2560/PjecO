using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject tankPrefab;
    public bool gameStart = false;
    private TransformVO data = null;

    public CinemachineVirtualCamera followCam;

    [HideInInspector]
    public int socketId;
    private PlayerRPC rpc;

    public object lockObj = new object();

    private Dictionary<int, PlayerRPC> playerList = new Dictionary<int, PlayerRPC>();
    public Transform[] spawnPos;

    private Queue<int> removeSocketQueue = new Queue<int>();

    private List<TransformVO> dataList;
    private bool needRefresh = false;

    public static int PlayerLayer;
    public static int EnemyLayer;

    public static Dictionary<TankCategory, TankDataVO> tankDataDic = new Dictionary<TankCategory, TankDataVO>();

    public Queue<HitInfoVO> hitQueue = new Queue<HitInfoVO>();
    public Queue<DeadVO> deadQueue = new Queue<DeadVO>();
    public Queue<int> respawnQueue = new Queue<int>();

    public CanvasGroup overPanel;
    public Text overText;


    public GameObject rankTxt;
    public GameObject rankPanel;
    public GameObject rankTxtParent;


    public static void InitGameData(string payload)
    {
        List<TankDataVO> list = JsonUtility.FromJson<TankDataListVO>(payload).tanks;

        foreach (TankDataVO t in list)
        {
            tankDataDic.Add(t.tank, t);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 게임매니저가 실행되고 있음. 확인 ㄱ");
        }
        instance = this;
        PoolManager.CreatePool<PlayerRPC>(tankPrefab, transform, 8);

        PlayerLayer = LayerMask.NameToLayer("PLAYER");
        EnemyLayer = LayerMask.NameToLayer("ENEMY");
    }

    public static void GameStart(TransformVO data)
    {
        /*Instantiate(instance.tankPrefab, data.position, Quaternion.identity);
        instance.socketId = data.socketId;*/
        lock (instance.lockObj)
        {
            instance.data = data;
        }

    }
    public static void SetRefreshData(List<TransformVO> list)
    {
        lock(instance.lockObj)
        {
            instance.dataList = list;
            instance.needRefresh = true;
        }
    }

    public static void DisconnectUser(int socketId)
    {
        lock (instance.lockObj)
        {
            instance.removeSocketQueue.Enqueue(socketId);
        }
    }
    
    private void Update()
    {
        lock (lockObj)
        {
            if(!gameStart && data != null)  //로그인 돼서 데이터 세팅됨
            {

                UIManager.CloseLoginPanel();
                rpc = PoolManager.GetItem<PlayerRPC>();
                socketId = data.socketId;
                InfoUI ui = UIManager.SetInfoUI(rpc.transform, data.name);
                rpc.InitPlayer(data.position, data.tank,ui ,false);
                followCam.Follow = rpc.transform;
                rankPanel.SetActive(true);
                
                gameStart = true;
            }
            if (needRefresh)
            {
                foreach(TransformVO tv in dataList)
                {
                    if (tv.socketId != socketId)
                    {
                        PlayerRPC p = null;
                        playerList.TryGetValue(tv.socketId, out p);
                        if (p == null)
                        {
                            p = MakeRemotePlayer(tv);
                        }
                        else
                        {
                            p.SetTransform(tv.position, tv.rotation, tv.turretRotation);
                        }
                    }
                }

                dataList.Sort((x, y) => x.kill.CompareTo(y.kill));
                ShowRank(dataList);
                needRefresh = false;
            } // end refresh

            while (removeSocketQueue.Count > 0)
            {
                int soc = removeSocketQueue.Dequeue();
                playerList[soc].SetDisable();
                playerList.Remove(soc);
            }

            while(hitQueue.Count>0)
            {
                HitInfoVO hit = hitQueue.Dequeue();
                PlayerRPC rpc = playerList[hit.socketId];
                rpc.SetHP(hit.hp);
            }

            while(deadQueue.Count>0)
            {
                DeadVO dead = deadQueue.Dequeue();
                PlayerRPC rpc = playerList[dead.socketId];
                rpc.SetDead();
                rpc.UIInvTest(false);
            }
            while(respawnQueue.Count>0)
            {
                int socId = respawnQueue.Dequeue();
                PlayerRPC rpc = playerList[socId];
                rpc.Respawn();
            }
        }

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            for(int i=0; i<playerList.Count; i++)
            {
                Debug.Log(playerList[i]);
            }
        }*/
    }  //end of update

    public PlayerRPC MakeRemotePlayer(TransformVO data)
    {
        PlayerRPC rpc = PoolManager.GetItem<PlayerRPC>();
        InfoUI ui = UIManager.SetInfoUI(rpc.transform, data.name);
        rpc.InitPlayer(data.position, data.tank, ui, true);
        
        playerList.Add(data.socketId, rpc);
        return rpc;
    }

    public PlayerRPC GetPlayerRPC(int socketId)
    {
        return playerList[socketId];
    }

    public static void RecordHitInfo(HitInfoVO vo)
    {
        lock(instance.lockObj)
        {
            instance.hitQueue.Enqueue(vo);
        }
    }
    public static void RecordDeadInfo(DeadVO vo)
    {
        lock (instance.lockObj)
        {
            instance.deadQueue.Enqueue(vo);
        }
    }
    public static void RecordRespawnInfo(int socId)
    {
        lock(instance.lockObj)
        {
            instance.respawnQueue.Enqueue(socId);
        }
    }

    /*public void PlayerDeath(int id)
    {
        playerList[id].SetDisable();
    }*/

    public void SetPlayerDead()
    {
        DOTween.To(
            () => overPanel.alpha,
            value => overPanel.alpha = value,
            1f,1f); 
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        for (int i = 10; i >= 0; i--)
        {
            overText.text = $"You died...\nWait for <color=#ff0000> {i} </color>sec to respawn";
            yield return new WaitForSeconds(1);
        }

        rpc.Respawn();

        DOTween.To(
            () => overPanel.alpha,
            value => overPanel.alpha = value,
            0f, 1f);
    }

    public void ShowRank(List<TransformVO> list)
    {
        int i;
        for( i=0; i<list.Count; i++)
        {
            rankTxtParent.transform.GetChild(i).gameObject.SetActive(true);
            rankTxtParent.transform.GetChild(i).GetComponent<Text>().text = list[i].name + " " + list[i].kill +" " +list[i].death;
        }
       
    }
}

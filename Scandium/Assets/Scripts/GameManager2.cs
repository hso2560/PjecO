using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    private PlayerMove player;
    private GameManager manager;
    [SerializeField] private GameObject door;
    [SerializeField] float dist = 0.5f;
    [SerializeField] Text cureTxt;
    [SerializeField] Sprite breakBox1, breakBox2;
    [SerializeField] Vector3[] camVec;
    [SerializeField] CameraMove cam;
    public GameObject playerWall1, playerWall2;  //메인 10에서 처음에 플레이어가 오브젝트들에게 말거는 것을 막아주는 벽들.
    public ForceObj med, rad;
    public GameObject Noise;
    private int sum;
    CardMng cardM;
    public InputField InputTxt;
    public GameObject InputObj;
    [SerializeField] SoundManager soundMng;
    private int touchBoxCount = 0;
    [SerializeField] GameObject Arr, Cu, St;  //메인10에서 화살,회복,돌덩이
    [SerializeField] GameObject stone;
    public Text nTxt;
    private bool isNtxt=false;

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMove>();
        cardM = FindObjectOfType<CardMng>();

        if (GameManager.proCount == 7)
            Invoke("CardSum", 1f);
    }

    void CardSum()
    {
        sum = cardM.r * 1000 + cardM.o * 100 + cardM.y * 10 + cardM.g;
    }
   
    void Update()
    {
        if(Vector2.Distance(player.transform.position,door.transform.position)<=dist)
        {
            if(GameManager.qCount==4)
            {
                GameManager.proCount = 2;
                SceneManager.LoadScene("Main3");
                GameManager.qCount = 5;
            }
        }
        if(GameManager.qCount==6)
        {
            if(GameManager.proCount==2)
                Invoke("Go4", 15f);
            if (player.isDie)
                CancelInvoke("Go4");
        }
       if(GameManager.proCount==9&&!cam.isMov)
        {
            if(cam.transform.position==camVec[0]||cam.transform.position == camVec[1] || cam.transform.position == camVec[2] || cam.transform.position == camVec[3])
            {
                manager.SendTaIma().SetActive(true);
            }
        }
    }

    public void OnNText()
    {
        if(!isNtxt)
        {
            isNtxt = true;
            nTxt.gameObject.SetActive(true);
            StartCoroutine(NNN());
        }
    }
    IEnumerator NNN()
    {
        for (float i = 0; i < 1; i += 0.01f)
        {
            i += 0.01f;
            nTxt.color = new Color(1, 0.1f, 0.1f, i);
            yield return new WaitForSeconds(0.02f);
        }
       
        Invoke("NN", 2.5f);
    }
    void NN()
    {
        StartCoroutine(NNNN());
    }
    IEnumerator NNNN()
    {

        for (float i = 1; i > 0; i -= 0.01f)
        {
            i -= 0.01f;
            nTxt.color = new Color(1, 0.1f, 0.1f, i);
            yield return new WaitForSeconds(0.02f);
        }
        
        nTxt.gameObject.SetActive(false);
    }

    void Go4()
    {
        GameManager.qCount = 7;
        GameManager.proCount = 3;
        SceneManager.LoadScene("Main4");
    }
    public void OnTxt()
    {
        cureTxt.color = Color.green;
        Invoke("gradually", 2f);
    }
    void gradually()
    {
        StartCoroutine(OffTxt());
    }
    IEnumerator OffTxt()
    {
        float i = 0.99f;
        while (i > 0)
        {
            cureTxt.color = new Color(0, 1, 0, i);
            i-=0.01f;
            yield return new WaitForSeconds(0.03f);
        }
    }
    public void Exit1()
    {
        ObjData objData = door.GetComponent<ObjData>();
        objData.id = 141;
    }
    public int GetSum()
    {
        return sum;
    }

    public void OnClickBox()
    {
        manager.ani.speed = 1;
        manager.ani.SetTrigger("box");
        soundMng.WoodSound();
        touchBoxCount++;
        if(touchBoxCount==10)
        {
            manager.boxBtn.image.sprite = breakBox1;
        }
        else if(touchBoxCount==20)
        {
            manager.boxBtn.image.sprite = breakBox2;
        }
        else if(touchBoxCount==30)
        {
            manager.GoStart = true;
            manager.ShowBoxImage.SetActive(false);
            manager.mainButt.interactable = true;
           // ObjData data = door.GetComponent<ObjData>();
           // data.enabled = false;
            door.layer = 0;
            med.gameObject.SetActive(true);  rad.gameObject.SetActive(true);
            med.ForceStart();    rad.ForceStart();
            
            Invoke("DelayBoxOff",1.5f);
        }
    }
    private void DelayBoxOff()
    {
        door.SetActive(false);   //여기서는 도어가 박스임
    }

    public void WallOff()  //메인 10에서의 안보이는 두 벽을 없앰
    {
        playerWall1.SetActive(false);
        playerWall2.SetActive(false);
    }

    public void CamMov(int t)
    {
        if(t==4)
        {
            manager.SendTaIma().SetActive(true);
            cam.isMov = true;
            return;
        }
        cam.isMov = false;
        while (true)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camVec[t], 6 * Time.deltaTime);  //while로 반복시키거나 update에서 실행시켜야지 이동함.
            if (cam.transform.position == camVec[t])
            {
                manager.SendTaIma().SetActive(true);
                break;
            }
        }
    }

    public void ItemUse(short c)
    {
        if(c==1)
        {
            Arr.layer = 0;
            Invoke("ItemCool1", 19);
        }
        else if(c==2)
        {
            Cu.layer = 0;
            Invoke("ItemCool2", 28);
        }
        else if(c==3)
        {
            St.layer = 0;
            Invoke("ItemCool3", 14);
        }
        
    }

    void ItemCool1()
    {
        Arr.layer = 10;
    }
    void ItemCool2()
    {
        Cu.layer = 10;
    }
    void ItemCool3()
    {
        St.layer = 10;
    }

    public void SetStone()
    {
        Instantiate(stone,new Vector3(12,0.1f,0),Quaternion.identity);
    }
}

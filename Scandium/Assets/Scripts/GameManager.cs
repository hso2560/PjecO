using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] TypingEffect _talkText;
    public SoundManager soundMng;
    [SerializeField] Text talkText;
    [SerializeField] Text nameText;
    public GameObject[] SelButt=new GameObject[2];
    [SerializeField] GameObject FirstEnemy; //몬스터 처음발견하는 장면에서 몬스터를 활성화하기 위함. 이 때는 몬스터의 애니메이션과 움직임이 없으므로 GameObject로 선언.
    [SerializeField] GameObject Water;  //물을 마시고 저장하면 물은 없는 상태에서 불러오기 해야하므로 가져옴.
    public Light2D light;
    public Text conText;

    public Text[] SelTxt = new Text[2];
    public GameObject scanObject;
    [SerializeField] private GameObject talkImage;
    private Sprite originSpr;
    private Color originCol;
    public bool isAction = false;

    [SerializeField] private TalkManager talkManager;
    private GameManager2 manager2;
    private CameraMove camMove;
    private PlayerMove player;
    public int talkIndex;
    [SerializeField] Image portrait;
    public GameObject NoiseImage;  //대화창에서 노이즈 일으키는 오브젝트
    public GameObject ShowBoxImage;

    public bool GoStart = false;
    private string[] FirstTalk;
    int t = 0;
    public int b = 0;  //어떤 물체들에게 대화를 해야만 다음구역으로 넘어가기위한 objData.Id를 변경시킬 때 이용
    bool preventT = true;
    bool otherSel = false;
    bool isPause = false;
    bool isPress = false;
    public bool fir = true;
    int tt = 0;
    public string s1;  //대화창의 대사(무언가의 선택에 의해 대사가 달라질 때 이용)
    string[] afMemo = new string[3];
    [SerializeField] private bool[] arrBool;
    public Button boxBtn;
    public Animator ani;

    public GameObject door;
    private short c;

    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject QuitMenu;
    [SerializeField] GameObject[] Buttons = new GameObject[2];
    [SerializeField] Boss boss;
    [SerializeField] Gun gun;

     public static int proCount=0;  //게임의 진행도('주로' 씬이 변경될 때마다)
    public static int qCount=0;  //퀘스트 번호
    public static int saveCount=0;  //저장 번호
    public static int sceneNum = 1;  //씬 번호
    public static int helpNum = 0;  //게임의 진행도(저장할 때 오브젝트나 텍스트 등을 관리하기 위함. 두번째 저장구간에서부터 쓰임)

    string myName= "<color=#008000ff>나</color>";
    string devName = "<color=#CB4900>개</color><color=#0000ffff>발</color><color=#008000ff>자</color>";

    public static int a = 0;   //저장 후에 계속해서 저장된 값을 씬 변경될 때마다 불러오는 것을 방지해준다.
    public Button mainButt;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        player = FindObjectOfType<PlayerMove>();
        originCol = this.portrait.color;
        originSpr = this.portrait.sprite;
        camMove = FindObjectOfType<CameraMove>();
        manager2 = FindObjectOfType<GameManager2>();
        Invoke("FirstSelf", 1f);
        Debug.Log(countManage);
        nameText.text = myName;

        GameLoad();  //데이터 불러오기
    }
   
    private void GameLoad()
    {
        if (!PlayerPrefs.HasKey("SAVE"))
            return;
        if (a < 1)
        {
            a++;
            saveCount = PlayerPrefs.GetInt("SAVE");
            qCount = PlayerPrefs.GetInt("QUEST");
            proCount = PlayerPrefs.GetInt("PROGRESS");
            sceneNum = PlayerPrefs.GetInt("SCENENUM");
            helpNum = PlayerPrefs.GetInt("HELP");
            player.GameLoad();

            if(proCount==8)
            {
                arrBool = new bool[3];
                for(int i=0; i<3; i++)
                {
                    arrBool[i] = false;
                }
            }
        }
    }
    public void StatusLoad()
    {
        if (saveCount > 0)
        {
            CancelInvoke("FirstSelf");
            preventT = false;
            GoStart = true;
            fir = false;
        }
        
    }

    public int countManage
    {
        get
        {
            return proCount;
        }
        set
        {
            proCount++;
        }
    }
    
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc,objData._name);
        talkImage.SetActive(isAction);
       
    }

    void Talk(int id, bool isNpc, string _name)
    {
        if (proCount != 10)
            player.sec = true;
        else if (proCount == 10)
            player.Freeze();
       
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            player.TalkSecN(3f);

            if (proCount == 10)
                player.FreezeOff();

            return;
        }

        if (isNpc)
        {
            portrait.color = originCol;
            talkText.text=talkData.Split(':')[0];
            portrait.sprite = talkManager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
            nameText.text = myName;
            nameText.text = _name;
            if(id==60)
            {
                if(talkIndex==1 || talkIndex==2||talkIndex==4||talkIndex==5)
                {
                    nameText.text = myName;
                }
            }
            else if(id==65)
            {
                if(talkIndex<12||talkIndex>14||talkIndex==13)
                    nameText.text = myName;

                if (talkIndex == 6 || talkIndex == 10||talkIndex==16)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                    if (talkIndex == 6)
                    {
                        nameText.text = "";
                        Invoke("sShake", 0.25f);
                        cShake();
                    }
                    else if(talkIndex==10)
                    {
                         SpriteRenderer dSp = door.GetComponent<SpriteRenderer>();
                         dSp.color = new Color(0, 0, 0, 1);
                        FirstEnemy.SetActive(true);
                    }
                    else if (talkIndex == 16)
                        nameText.text = "";
                }
                else if (talkIndex == 7)
                {
                    sShake();
                    Invoke("sShake", 0.25f);
                    cShake();
                }
            }
            else if (id == 260)
            {
                arrBool[2] = true;
                if (talkIndex ==2 || talkIndex==4 || talkIndex==6)
                {
                    nameText.text = ""; portrait.color = new Color(1, 1, 1, 0);
                }
                if (talkIndex <= 1 || talkIndex == 3 || talkIndex==5 || talkIndex==7||talkIndex==9||talkIndex==11||talkIndex==13||talkIndex>=16)
                    nameText.text = myName;
                if (talkIndex == 3)
                {
                    soundMng.Sound0();
                    NoiseImage.SetActive(true);
                }
                else if(talkIndex==10)
                {
                    manager2.Noise.SetActive(true);
                    light.pointLightInnerRadius = 0;
                    light.pointLightOuterRadius = 18;
                    soundMng.Sound1();
                    Invoke("NoiseOff", 0.3f);
                    Invoke("ReSound", 0.3f);
                }
            }
            else if(id==300)
            {
                manager2.WallOff();
                if(talkIndex==0 || talkIndex==2||talkIndex==3 || talkIndex == 5 || talkIndex == 7 || talkIndex == 9 || talkIndex >= 11)
                    nameText.text = myName;
                
                else if(talkIndex==10)
                {
                    manager2.Noise.SetActive(true);
                    soundMng.Sound1();
                    Invoke("NoiseOff", 0.3f);
                    boss.startNum = 1;
                }
            }
            else if(id==340)
            {
                if(talkIndex==6)
                {
                    for (int i = -90; i <= 180; i += 90)
                    {
                        GameObject arrow = PoolManager.instance.GetQueue();
                        arrow.transform.position = player.transform.position;
                        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i));
                    }
                }
                if(talkIndex<=2||talkIndex==4||talkIndex==5|| talkIndex == 8 || talkIndex == 9 || talkIndex == 11 || talkIndex == 12 || talkIndex == 14 || talkIndex == 16 || talkIndex == 19 || talkIndex == 20 || talkIndex == 23 || talkIndex == 25 || talkIndex ==28)
                {
                    nameText.text = myName;
                    if(talkIndex==19)
                    {
                        Invoke("sShake", 0.25f);
                        cShake();
                    }
                    else if(talkIndex==28)
                        player.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else if(talkIndex==18)
                {
                    Water.SetActive(true);  FirstEnemy.SetActive(true);  //적 두마리 소환 (코드 재활용)
                }
                if(talkIndex==24)
                {
                    soundMng.Sound0();
                    NoiseImage.SetActive(true);
                }
            }
        }
        else
        {
            portrait.sprite = originSpr;
            portrait.color = originCol;
            talkText.text = talkData;
            nameText.text = myName;

            if (id == 70)
            {
                if (talkIndex == 3)
                {
                    portrait.color = new Color(1, 1, 1, 0);

                }
            }
            else if (id == 11)
            {
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "지금 나간다.";
                SelTxt[1].text = "더 둘러본다.";
            }
            else if (id == 100)
            {
                if (talkIndex == 1 || talkIndex == 3 || talkIndex == 10)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                }
            }
            else if (id == 101)
            {
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "출발한다";
                SelTxt[1].text = "역시 지금은 아니야";
            }
            else if (id == 110)
            {
                if (talkIndex == 2)
                    portrait.color = new Color(1, 1, 1, 0);
            }
            else if (id == 115)
            {
                if (talkIndex == 1)
                    portrait.color = new Color(1, 1, 1, 0);
            }
            else if (id == 1000 || id == 1001 || id==1002 ||id==1003)   //저장
            {
                portrait.color = new Color(1, 1, 1, 0);
                nameText.text = "";
                otherSel = true;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                mainButt.interactable = false;
                SelTxt[0].text = "저장한다.";
                SelTxt[1].text = "저장하지않는다.";
            }
            else if (id == 120)
            {

                if (talkIndex == 1 || talkIndex == 2)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                }
            }
            else if (id == 141)
            {
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "간다.";
                SelTxt[1].text = "좀 더 살펴본다.";
            }
            else if (id == 171||id==241)
            {
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "출발한다";
                SelTxt[1].text = "조금 더 쉬고 간다";
            }
            else if (id == 190)
            {
                if (talkIndex <= 4)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                    nameText.text = "";
                }
            }
            else if (id == 221)
            {
                manager2.InputObj.SetActive(true);
                if (talkIndex == 1)
                {
                    talkText.text = s1;
                    manager2.InputObj.SetActive(false);
                }
            }
            else if(id==191)
            {
                if(talkIndex<=2)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                    nameText.text = "";
                }
            }
            else if (id == 222)
            {
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "간다.";
                SelTxt[1].text = "좀 더 살펴본다.";
            }
            else if(id==230)
            {
                arrBool[0] = true;
                if (talkIndex <= 7)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                    nameText.text = "";
                    if (talkIndex == 6)
                    {
                        manager2.Noise.SetActive(true);
                        soundMng.Sound1();
                        Invoke("NoiseOff", 0.3f);
                        Invoke("ReSound", 0.3f);
                    }
                    else if (talkIndex == 4)
                    {
                        soundMng.Sound0();
                        NoiseImage.SetActive(true);
                    }
                }
                else if (talkIndex == 8)
                {
                    soundMng.SoundStop();
                    NoiseImage.SetActive(false);
                }
            }
            else if(id==236)
            {
                if (talkIndex <= 1)
                {
                    soundMng.Sound0();
                    NoiseImage.SetActive(true);
                }
                else
                {
                    NoiseImage.SetActive(false);
                    manager2.Noise.SetActive(true);
                    soundMng.Sound1();
                    Invoke("NoiseOff", 0.3f);
                }
            }
            else if(id==250)
            {
                if(talkIndex==3)
                {
                    portrait.color = new Color(1, 1, 1, 0);
                    nameText.text = "";
                    GoStart = false;                                       //boxSpr = boxBtn.GetComponent<Button>().image.sprite;
                }
            }
            else if(id==255)
            {
               arrBool[1] = true;
            }
            else if(id==280)
            {
                c = 1;  //화살
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "예";
                SelTxt[1].text = "아니요";
            }
            else if(id==290)
            {
                c = 2; //체력
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "예";
                SelTxt[1].text = "아니요";
            }
            else if (id == 270)
            {
                c = 3; //돌덩이
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "예";
                SelTxt[1].text = "아니요";
            }
            else if(id==310)
            {
                c = 4; //출구
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "나간다";
                SelTxt[1].text = "안나간다";
            }
            else if(id==320)
            {
                mainButt.interactable = false;
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(true);
                }
                SelTxt[0].text = "간다";
                SelTxt[1].text = "조금 더 휴식하고";
            }
        }
        isAction = true;
        talkIndex++;
    }

    void ReSound()
    {
        soundMng.Sound0();
    }

    void NoiseOff()
    {
        manager2.Noise.SetActive(false);
    }
    void NoiseImageOff()
    {
        NoiseImage.SetActive(false);
    }

    private void cShake()
    {
        camMove.Shake();
    }
    private void sShake()
    {
        camMove.StopShake();
    }


    public void ClickAcc()
    {
        mainButt.interactable = true;
        if (proCount == 0)
        {
            SceneManager.LoadScene("Main2");
            proCount = 1;
            qCount=2;
        }
        else if(proCount==4)
        {
            if(otherSel)
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                otherSel = false;
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
                saveCount = 1;
                sceneNum = 5;
                Save();
                a = 1;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
                qCount = 8;
                proCount = 5;
                SceneManager.LoadScene("Main6");
            }
        }
        else if(proCount==5)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
            proCount = 6;
            qCount = 9;
            SceneManager.LoadScene("Main7");
        }
        else if(proCount==6)
        {
            if (otherSel)
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                otherSel = false;
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
                saveCount = 2;
                sceneNum = 7;
                Save();
                a = 1;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
                qCount = 10;
                proCount = 7;
                SceneManager.LoadScene("Main8");
            }
        }
        else if(proCount==7)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            otherSel = false;
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
            proCount = 8;
            qCount = 11;
            SceneManager.LoadScene("Main9");
        }
        else if(proCount==8)
        {
            if (otherSel)
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
                saveCount = 3;
                sceneNum = 9;
                Save();
                a = 1;
                otherSel = false;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
                qCount = 12;
                proCount = 9;
                SceneManager.LoadScene("Main10");
            }
        }
        else if(proCount==10)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;

            if (c == 1)
                player.ArrowManage = Random.Range(40, 61);
            else if (c == 2)
                player.HpManage = Random.Range(50, 91);
            else if (c == 3)
                manager2.SetStone();
            else if(c==4)
            {
                qCount = 15;
                proCount = 11;
                SceneManager.LoadScene("Main11");
            }
            if (c <= 3)
                manager2.ItemUse(c);
        }
        else if(proCount==11)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
            if (otherSel)
            {
                otherSel = false;
                saveCount = 4;
                sceneNum = 11;
                Save();
                a = 1;
            }
            else
            {
                proCount = 12;
                SceneManager.LoadScene("Main12");  //엔딩 씬으로
            }
        }
    }
    public void ClickRef()
    {
        mainButt.interactable = true;
        if (proCount == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
        }
        else if(proCount==4)
        {
            if (otherSel)
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                otherSel = false;
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
            }
        }
        else if (proCount == 5)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
        }
        else if(proCount==6)
        {
            if (otherSel)
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                otherSel = false;
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
            }
        }
        else if(proCount==7)
        {
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
        }
        else if(proCount==8)
        {
            if(otherSel)
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                otherSel = false;
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;
            }
        }
        else if(proCount==10)
        {   
                for (int i = 0; i < 2; i++)
                {
                    SelButt[i].SetActive(false);
                }
                talkImage.SetActive(false);
                isAction = false;
                talkIndex = 0;     
        }
        else if(proCount==11)
        {
            if (otherSel)
                otherSel = false;
            for (int i = 0; i < 2; i++)
            {
                SelButt[i].SetActive(false);
            }
            talkImage.SetActive(false);
            isAction = false;
            talkIndex = 0;
        }
    }
    
    public void FirstSelf()
    {
        preventT = false;
        if (proCount == 0)
        {
            FirstTalk = new string[6];
            FirstTalk[0] = "(....)";
            FirstTalk[1] = "(내가 왜 여기에 있지?..)";
            FirstTalk[2] = "(이곳은 처음 보는 방이다..)";
            FirstTalk[3] = "(일단 한 번 이곳을 <color=#0000ffff>조사</color>해봐야겠다.)";
            FirstTalk[4] = "왼쪽 하단의 조작키로 플레이어를 움직일 수 있고 오른쪽 하단의 …버튼으로 대화를 진행할 수 있습니다.";
            FirstTalk[5] = "오른쪽 상단의 메뉴버튼으로 일시정지와 게임나가기를 할 수 있고 퀘스트 버튼을 통해서 현재 진행중인 퀘스트를 확인할 수 있습니다.";
           
        }
        else if(proCount==1)
        {
            if (qCount == 2)
            {
                FirstTalk = new string[2];
                FirstTalk[0] = "(분위기가 으스스해..)";
                FirstTalk[1] = "(역시 빨리 나가야겠어)";
            }
            else
            {
                portrait.color = originCol;
                fir = true;
                GoStart = false;
                player.transform.position = new Vector2(20f, 18.8f);
                FirstEnemy.SetActive(true);
                FirstTalk = new string[4];
                FirstTalk[0] = "(저..저건 뭐야?!!..)";
                FirstTalk[1] = "(뭐지.. 왜 이런 일이 있는거지....)";
                FirstTalk[2] = "(일단...튀어야 해... 빨리.. 어딘가로...)";
                FirstTalk[3] = "(그래.. 일단 내가 아까 있었던 곳으로 빨리 도망쳐야겠다.)";
            }
        }
        else if(proCount==2)
        {
            FirstTalk = new string[6];
            FirstTalk[0] = "(아까 전 <color=#ff0000ff>그것</color>이 이쪽에 다시 오기전에 문을 잠가야겠다.)";
            FirstTalk[1] = "(.........)";
            FirstTalk[2] = "(아니... 이 문에는 잠금장치가 없어....)";
            FirstTalk[3] = "(어떡하지?... 빨리... 무언가를....!)";
            FirstTalk[4] = "(맞다! 아까 이 방에는 <color=#0000ffff>밧줄</color>이 있었어)";
            FirstTalk[5] = "(그걸 이용하면 문을 막을 수 있을것 같다. 한 번 시도해보자)";
        }
        else if(proCount==3)
        {
            FirstTalk = new string[3];
            FirstTalk[0] = "하.....하....";
            FirstTalk[1] = "(일단 빠져나왔다.)";
            FirstTalk[2] = "(아직 완전히 따돌리지는 못했으니 빨리 다른 길을 찾아봐야겠다..)";
        }
        else if(proCount==4)
        {
            FirstTalk = new string[6];
            FirstTalk[0] = "(일단 그 녀석한테서 도망치는데에는 성공한건가?)";
            FirstTalk[1] = "(그나저나 여기는 어둡네...)";
            FirstTalk[2] = "(한 숨 돌리긴 했지만 그래도 여긴 안심할 수 없는 곳이야.. 보통 다른 사람들이라면 기절하겠는데?)";
            FirstTalk[3] = "(그런데...)";
            FirstTalk[4] = "(다른사람들이 누구인지.. <color=#0000ffff>기억이 안나....</color>)";
            FirstTalk[5] = "이 어두운 장소에 어떤 내용이 적힌 종이가 있을지도 모르니 잘 확인하는 것이 좋습니다.";
        }
        else if(proCount==5)
        {
            FirstTalk = new string[6];
            FirstTalk[0] = "(이제부터는 진짜 목숨을 걸어야 돼.. 조심하자...)";
            FirstTalk[1] = "(으... 그래도 역시 떨리네.)";
            FirstTalk[2] = "(근데 아까도 봤지만... 역시 저 괴물들은 좀 전의  <color=#0000ffff>'그것'</color>과 거의 같은데 상처가 더 없어)";
            FirstTalk[3] = "(분명 아까 그것에겐 칼이 꽂혀있었는데... 혹시 <color=#0000ffff>다른 누군가</color>가 칼로 공격한건가?)";
            FirstTalk[4] = "(그렇다면 여기에는 나 이외의 다른 사람도 있는건가? 일단 이 생각은 뒤로하고 우선 저 괴물들의 속도가 상처가 더 없는만큼 아까 그것보다 좀 더 <color=#ff0000ff>빠르니까</color> 조심해야겠어)";
            FirstTalk[5] = "이곳에서는 대화중일 때와 대화가 끝나고 3초간은 무적입니다.(단, 이 대화창은 제외)";
        }
        else if(proCount==6)
        {
            FirstTalk = new string[5];
            FirstTalk[0] = "하.....";
            FirstTalk[1] = "(드디어 안전해보이는 구역으로 왔다.)";
            FirstTalk[2] = "(주변이 어두워서 뚫는 것이 엄청 힘들었어..)";
            FirstTalk[3] = "(여기는 좀 밝네. 게다가 이곳엔 <color=#0000ffff>쓸만한 것</color>도 있어보여. 챙기면 좋을 것 같아.)";
            FirstTalk[4] = "(일단 좀 쉬고 여기를 둘러보고 다음 구역으로 넘어가야겠다.)";
        }
        else if(proCount==7)
        {
            FirstTalk = new string[7];
            FirstTalk[0] = "(그래도 아까 그곳보다는 이곳이 더 밝네..)";
            FirstTalk[1] = "(이번에도 역시 조심히 잘 빠져나가야 해..)";
            FirstTalk[2] = "(이번엔 그래도 무기가 있으니까 위험한 상황에서 쓰면 도움이 많이 될 것 같아)";
            FirstTalk[3] = "대화 버튼 옆의 공격버튼으로 화살을 날려서 공격을 할 수 있습니다.";
            FirstTalk[4] = "버튼 안의 숫자는 남은 화살의 수를 나타내며 화살의 재발사 대기시간은 0.5초입니다.";
            FirstTalk[5] = "대화중일 때와 대화가 끝나고 3초 간은 무적입니다.(단, 이 대화창은 제외)";
            FirstTalk[6] = "ㄱㄱ";
            player.ArrowManage = 15;
        }
        else if(proCount==8)
        {
            FirstTalk = new string[3];
            FirstTalk[0] = "후....";
            FirstTalk[1] = "(무사히 비밀번호를 맞추고 들어왔다. 여기는 또 안전해 보이는 구역인거 같네)";
            FirstTalk[2] = "(일단 여기서 쉬었다가 다시 출발하자. 도대체 언제쯤 되어야 여기서 나갈 수 있을지...)";

            arrBool = new bool[3];
            for (int i = 0; i < 3; i++)
                arrBool[i] = false;
        }
        else if(proCount==9)
        {
            if (qCount == 12)
            {
                FirstTalk = new string[2];
                FirstTalk[0] = "?? (저기에 사람(?)이 있는것 같은데?..)";
                FirstTalk[1] = "(혹시 나와 같은 <color=#0000ffff>피해자</color>인가? 한 번 말을 걸어보자)";
            }
            else
            {
                portrait.color = new Color(0, 0, 0, 0);
                nameText.text = "";
                FirstTalk = new string[6];
                FirstTalk[0] = "앞에 있는 귀신같이 생긴 놈을 처치해야 합니다. 이 곳에서는 대화창이 열려도 데미지를 받습니다.(단, 해당 대화창에서는 공격을 받지않습니다)";
                FirstTalk[1] = "이 곳과 상호작용을 하면 화살을 보충할 수 있습니다.(쿨타임: 20초)";
                FirstTalk[2] = "이 곳과 상호작용을 하면 체력을 회복할 수 있습니다.(쿨타임: 30초)";
                FirstTalk[3] = "이 곳과 상호작용을 하면 돌덩이를 던질 수 있습니다.(쿨타임: 15초)";
                FirstTalk[4] = "이 곳에서는 외부에서의 공격이 플레이어에게 향합니다. 메세지가 뜨면 화살로 부숴주세요.";
                FirstTalk[5] = "ㄱㄱㄱㄱㄱㄱㄱㄱㄱㄱ";
            }
        }
        else if(proCount==11)
        {
            FirstTalk = new string[4];
            FirstTalk[0] = "하.....하.....후우.......";
            FirstTalk[1] = "(방금 그 놈은 대체 뭐야?... 아까의 잡졸들과는 다르게 너무 위험했어...)";
            FirstTalk[2] = "(하마터면 진짜로 죽을 뻔했다... 저런 놈을 만든 새끼의 면상이 궁금하네)";
            FirstTalk[3] = "(일단 조금 쉬었다가 가야겠다..)";
        }
        else if(proCount==12)
        {
            GoStart = true;
            fir = false;
            return;
        }
        talkImage.SetActive(true);
        talkText.text=FirstTalk[t];

    }
    
    public void ClickTalk()
    {
        if (!preventT)
        {
            if (t <= FirstTalk.Length-2)  //count==0기준:t<=3이라고 생각할 수도 있지만 클릭 했을 때 기준으로 t<=2 일때 t++을 실행하므로 토크이미지는 닫히지 않고 t=3인 상태에서 클릭을 하면 모든 처음 독백이 끝난다.
            {
                t++;
                talkText.text = FirstTalk[t];
                if (t >= 4)
                {
                    if (proCount == 0)
                    {
                        portrait.color = new Color(1, 1, 1, 0);
                        nameText.text = "";
                    }
                    else if(proCount==4)
                    {
                        if(t==5)
                        {
                            portrait.color = new Color(1, 1, 1, 0);
                            nameText.text = "";
                        }
                    }
                    else if(proCount==5)
                    {
                        if(t==5)
                        {
                            portrait.color = new Color(1, 1, 1, 0);
                            nameText.text = "";
                        }
                    }
                }
                if (proCount == 7)
                {
                    if (t >= 3)
                    {
                        portrait.color = new Color(1, 1, 1, 0);
                        nameText.text = "";
                        if (t == 6)
                        {
                            Invoke("sShake", 0.3f);
                            cShake();
                        }
                    }
                }
                else if (proCount == 9)
                {
                    if (qCount == 13)
                    {
                        if (t > 0)
                        {
                            talkImage.SetActive(false);
                            manager2.CamMov(t - 1);
                        }
                    }
                }
            }
            else if(t==FirstTalk.Length-1)
            {
                talkText.text = "";
                talkImage.SetActive(false);
                GoStart = true;
                fir = false;
                t = 0;
                if(proCount==4)
                {
                    player.HpManage = 180;
                    manager2.OnTxt();
                }
                else if (proCount == 6)
                {
                    player.HpManage = 120;
                    player.IncMaxHp(50);
                    manager2.OnTxt();
                }
                else if(proCount==8)
                {
                    player.HpManage = 120;
                    manager2.OnTxt();
                }
                else if(proCount==9)
                {
                    if(qCount==13)
                    {
                        proCount = 10;
                        boss.startNum = 2;
                        player.AttackBtn.interactable = true;
                        gun.StartGun();
                        boss.light.pointLightInnerRadius = 6;
                        boss.light.pointLightOuterRadius = 9;
                        //Debug.Log("TestSuccess");
                    }
                }
            }
           
        }
    }
    
    public GameObject SendTaIma()
    {
        return talkImage;
    }

    public void AfterMemo()
    {
        afMemo[0] = "(뭘 찾으라는 거지? 그리고 내용의 의미가 <color=#ff0000ff>중의적</color>이다.";
        afMemo[1] = "(한 눈에 봐도 여기서 챙길만 한 것은 없다.)";
        afMemo[2] = "(더 있어봤자 좋을건 없다. 여기서 <color=#0000ffff>나가보자</color>)";
        talkImage.SetActive(true);
        talkText.text=afMemo[tt];

    }

    public void ClickTalk2()
    {
        if(tt<=1)
        {
            tt++;
            talkText.text = afMemo[tt];
        }
        else
        {
            talkText.text="";
            talkImage.SetActive(false);
            talkManager.isMemo = false;
            tt = 0;
            qCount=1;
            ObjData objData = door.GetComponent<ObjData>();
            objData.id = 11;
        }
    }

    public void CameraTalk()  //메인 10에서 보스전 시작 바로전에 설명하는 대화창
    {
        GoStart = false;
        fir = true;
        FirstSelf();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!player.isDie)
            { 
                    if (!isPause)
                    {
                        OnClickMenu();
                    }
                    else
                    {
                        if (!isPress)
                        {
                            OnClickBack();
                        }
                        else
                        {
                            ClickNo();
                        }
                    }
            }
        }
       
        if(proCount==6)
        {
            if(b==2)
            {
                ObjData objData = door.GetComponent<ObjData>();
                objData.id = 171;
            }
            if(helpNum==1)
            {
                Water.SetActive(false);
            }
        }
        else if(proCount==8)
        {
            if(arrBool[1]&& arrBool[2])
            {
                if (arrBool[0])
                {
                    ObjData data = door.GetComponent<ObjData>();
                    data.id = 241;
                }
            }
            if(helpNum==2)
            {
                Water.SetActive(false);   //여기서는 물이 아니라 고기가 사라지는거임
            }
        }
    }
    public void OnClickMenu()
    {
        isPause = true;

        if (qCount != 14)
            Time.timeScale = 0;
        else if (qCount == 14)
            Time.timeScale = 0.1f;

        Menu.SetActive(true);
    }
    public void OnClickQuit()
    {
        isPress = true;
        QuitMenu.SetActive(true);
        for(int i=0; i<2; i++)
        {
            Buttons[i].SetActive(false);
        }
    }
    public void ClickYes()
    {
        a = 0;
        isPause = false;
        isPress = false;
        Time.timeScale = 1;
        if(saveCount==0)
        {
            qCount = 0;
            proCount = 0;
        }
        SceneManager.LoadScene("Lobby");
    }
    public void ClickNo()
    {
        isPress = false;
        QuitMenu.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            Buttons[i].SetActive(true);
        }
    }
    public void OnClickBack()
    {
        isPause = false;
        Menu.SetActive(false);
        Time.timeScale = 1;
        if(qCount==14)
           manager2.OnNText();
    }
    public void Quest1(GameObject Spot)
    {
        scanObject = Spot;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc, objData._name);
        talkImage.SetActive(isAction);
    }
    private void Save()   //저장
    {
        PlayerPrefs.SetInt("SAVE", saveCount);
        PlayerPrefs.SetInt("QUEST", qCount);
        PlayerPrefs.SetInt("PROGRESS", proCount);
        PlayerPrefs.SetInt("SCENENUM", sceneNum);
        PlayerPrefs.SetInt("HELP", helpNum);
        player.SaveHp();
      
    }

    public void BossMenu()
    {
        Buttons[1].SetActive(false);
        Buttons[0].transform.position+=new Vector3(0,-1,0);
        conText.text = "계속하기 계속하기 계속하기 계속하기 계속하기 계속하기 계속죽기";
        conText.color = new Color(1, 0, 0);
    }
}




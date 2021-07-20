using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
   
    [SerializeField] private float Speed = 5.5f;

    private static int maxHp=300;  //현재 최대HP 저장
    
    private static int hp;  //현재 HP저장
    private float speed;

    private bool right = false;
    private bool up = false;
    private bool down = false;
    private bool left = false;

    private bool isDamaged = false;
    private float rot = 0;

    public bool isDie = false;
    public bool sec = false;

    private bool isAttack = false;
    private float AttackDelay = 0.5f;
    private static int arrowCount = 0;  //현재 화살 개수 저장

    private SpriteRenderer Sp;
    private Collider2D Col;
    private Rigidbody2D rigid;
    private Vector3 dirVec;
    private Color icolor;

    public Button AttackBtn;

    private Animator ani;

    [SerializeField] private TalkManager talkManager;
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private Text arrowTxt;
    [SerializeField] private UIEffect uiManager;
    public GameObject door;
    
    public GameObject scanObject;

    private void Start()
    {
        Sp = GetComponent<SpriteRenderer>();
        Col = GetComponent<Collider2D>();   
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        ani.Play("DownIdle");
       
        if (GameManager.proCount == 0)
        {
            hp = 300;
            maxHp = 300;
        }
        speed = Speed;
        icolor = this.Sp.color;
       
       // GameLoad();   //GM에서 호출함
    }

    public void GameLoad()  //데이터 불러오기
    {
        if (!PlayerPrefs.HasKey("HP"))
            return;

        hp = PlayerPrefs.GetInt("HP");
        maxHp = PlayerPrefs.GetInt("MAXHP");
        arrowCount = PlayerPrefs.GetInt("ARROW");

        if (GameManager.saveCount == 1)
        {
            transform.position = new Vector2(5, 75);
        }
        else if(GameManager.saveCount==2)
        {
            transform.position = new Vector2(2, 15);
        }
        else if(GameManager.saveCount==3)
        {
            transform.position = new Vector2(0, 16);
        }
        else if(GameManager.saveCount==4)
        {
            transform.position = new Vector2(-0.6f, 5.8f);
        }
        manager.StatusLoad();

        if (GameManager.proCount >= 9)
            arrowTxt.text = arrowCount.ToString();
        
    }
    
    private void Update()
    {
        if (manager.GoStart)
        {
            if (manager.isAction == false)
            {
                if (!talkManager.isMemo)
                {
                    if (up)
                    {
                        transform.Translate(Vector2.up * speed * Time.deltaTime);
                        dirVec = Vector3.up;
                        rot = 0;
                        ani.Play("PlayerUp");
                    }
                    if (right)
                    {
                        transform.Translate(Vector2.right * speed * Time.deltaTime);
                        dirVec = Vector3.right;
                        rot = 270;
                        ani.Play("PlayerRight");
                    }
                    if (left)
                    {
                        transform.Translate(Vector2.left * speed * Time.deltaTime);
                        dirVec = Vector3.left;
                        rot = 90;
                        ani.Play("PlayerLeft");
                    }
                    if (down)
                    {
                        transform.Translate(Vector2.down * speed * Time.deltaTime);
                        dirVec = Vector3.down;
                        rot = 180;
                        ani.Play("PlayerDown");
                    }
                    /* float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
                     float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
                     Vector2 movDir = Input.GetButton("Horizontal") ? new Vector2(x,0) : new Vector2(0,y);
                     transform.Translate(movDir);*/

                    //PC버전 캐릭터 움직임
                   /* if (Input.GetKey(KeyCode.UpArrow))
                        up = true;
                    else if (Input.GetKey(KeyCode.RightArrow))
                        right = true;
                    else if (Input.GetKey(KeyCode.LeftArrow))
                        left = true;
                    else if (Input.GetKey(KeyCode.DownArrow))
                        down = true;
                    if(Input.GetButtonUp("Horizontal"))
                    {
                        if (right)
                            ClickRight2();
                        else if (left)
                            ClickLeft2();
                    }
                    else if(Input.GetButtonUp("Vertical"))
                    {
                        if (up)
                            ClickUp2();
                        else if (down)
                            ClickDown2();
                    }*/
                }
            }
        }
        if(hp>maxHp)
        {
            hp = maxHp;
        }
        else if(hp<=0)
        {
            hp = 0;
            Die();
        }

        if(sec)    //플레이어와 적의 충돌만 무시하기
        {
            Physics2D.IgnoreLayerCollision(8, 9, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }

        if(GameManager.proCount>=7)
           arrowTxt.text = arrowCount.ToString();

        //PC 전용
       /* if (Input.GetKeyDown(KeyCode.Space))
            Inves();
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
            Attack();*/
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(rigid.position, dirVec * 1.3f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.3f, LayerMask.GetMask("Object"));
        
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
       
    }
    public void Die()  //사망
    {
        isDie = true;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        Sp.color = new Color(0.8f, 0, 0);
        diePanel.SetActive(true);
    }
    public void IncMaxHp(int value)
    {
        maxHp += value;
    }
    public int HpManage
    {
        get
        {
            return hp;
        }
        set
        {
            hp += value;
        }
    }
    public void Damage(int value)
    {
        hp -= value;
    }
    public int MaxHP()
    {
        return maxHp;
    }
    public void SpeedUp(float value)
    {
        speed += value;
        Speed += value;
    }
    public void SpeedDown(float value)
    {
        Speed -= value;
        speed -= value;
    }
    public void Inves()  //대화/조사
    {
        if (!talkManager.isMemo)
        {
            if (!manager.fir)
            {
                if (scanObject != null)
                {
                    manager.Action(scanObject);
                }
            }
            else
            {
                manager.ClickTalk();
                
            }
        }
        else
        {
            manager.ClickTalk2();
        }
    }

    public void Attack()  //화살 공격
    {
        if (arrowCount > 0)
        {
            if (isAttack)
            {
                return;
            }
            isAttack = true;
            GameObject arrow = PoolManager.instance.GetQueue();
            arrow.transform.position = transform.position;
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
            uiManager.t = 0.5f;
            uiManager.FuncTFlow();
            arrowCount--;
            Invoke("WaitAttack", AttackDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bullet")
        {
            BulletManager bullet = collision.GetComponent<BulletManager>();
            if (!sec)
                Damaged(bullet.SendPower());

            if (bullet.bulletId == 1)
                PoolManager.instance.InsertQueue2(bullet.gameObject);
            else if (bullet.bulletId == 3)
                PoolManager.instance.InsertQueue3(bullet.gameObject);
        }
        if(collision.tag=="Batk")
        {
            ShortAtk sa = collision.GetComponent<ShortAtk>();

            if (!sec)
            {
                Damaged(sa.Str1);
            }
        }
    }

    private void WaitAttack()
    {
        isAttack = false;
    }

   //이동 버튼
    public void ClickUp()
    {
        up = true;
       
    }
    public void ClickUp2()
    {
        up = false;
        ani.Play("UpIdle");
    }
    public void ClickRight()
    {
        right = true;
        
    }
    public void ClickRight2()
    {
        right = false;
        ani.Play("RightIdle");
    }
    public void ClickLeft()
    {
        left = true;
        
    }
    public void ClickLeft2()
    {
        left = false;
        ani.Play("LeftIdle");
    }
    public void ClickDown()
    {
        down = true;
       
    }
    public void ClickDown2()
    {
        ani.Play("DownIdle");
        down = false;
    }
    public void Damaged(int value)   //플레이어가 데미지를 받음
    {
        if (isDamaged)
            return;

        isDamaged = true;
        sec = true;
        hp -= value;
        StartCoroutine(DamageEffect());
    }
    public void DieLobby()  //죽고나서 로비로 가기
    {
        GameManager.a = 0;
        isDie = false;
        diePanel.SetActive(false);
        hp = 300;
        Sp.color = icolor;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (GameManager.saveCount == 0)
        {
            maxHp = 300;
            GameManager.proCount = 0;
            GameManager.qCount = 0;
            GameManager.helpNum = 0;
            arrowCount = 0;
        }
        else if (GameManager.saveCount >= 1)
        {
            PlayerPrefs.SetInt("HP", hp);
        }
        
        SceneManager.LoadScene("Lobby");
    }
    public void Revive()  //부활
    {
         GameManager.a= 0;
        isDie = false;
        hp = 300;
        Sp.color = icolor;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        diePanel.SetActive(false);
        if(GameManager.saveCount==0)
        {
            maxHp = 300;
            GameManager.proCount = 0;
            GameManager.qCount = 0;
            GameManager.helpNum=0;
            SceneManager.LoadScene("Main1");
        }
        else if(GameManager.saveCount==1)
        {
            PlayerPrefs.SetInt("HP", hp);
            GameManager.proCount = 4;
            GameManager.qCount = 7;
            GameManager.helpNum = 0;
            SceneManager.LoadScene("Main5");
            
        }
        else if(GameManager.saveCount==2)
        {
            PlayerPrefs.SetInt("HP", hp);
            GameManager.proCount = 6;
            GameManager.qCount = 9;
            GameManager.helpNum = PlayerPrefs.GetInt("HELP");
            arrowCount = 0;
            SceneManager.LoadScene("Main7");
        }
        else if(GameManager.saveCount==3)
        {
            PlayerPrefs.SetInt("HP", hp);
            GameManager.proCount = 8;
            GameManager.qCount = 11;
            GameManager.helpNum = PlayerPrefs.GetInt("HELP");
            arrowCount = PlayerPrefs.GetInt("ARROW");
            SceneManager.LoadScene("Main9");
        }
    }
    private IEnumerator DamageEffect()
    {
        speed-=0.5f;

        int c = 0;
        while (c < 3)
        {
            Sp.color = new Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds(0.2f);
            Sp.color = icolor;
            yield return new WaitForSeconds(0.2f);
            ++c;
        }

        isDamaged = false;
        speed+=0.5f;

        if (GameManager.proCount != 10)
        {
            if (!manager.isAction)
                sec = false;
        }
        else
            sec = false;
    }
    public void SaveHp()  //저장
    {
        PlayerPrefs.SetInt("HP", hp);
        PlayerPrefs.SetInt("MAXHP", maxHp);
        PlayerPrefs.SetInt("ARROW", arrowCount);
    }
    
    public void TCol() 
    {
        Col.enabled = true;
    }
    public void TColN()  
    {
        Col.enabled = false;
    }
    public void TalkColOn(float t)
    {
        Invoke("TCol", t);
    }
    public void TalkSecN(float t)
    {
        Invoke("SecFalse", t);
    }
    private void SecFalse()
    {
        if (manager.isAction)
            return;

        sec = false;
    }
    
    public int ArrowManage
    {
        get
        {
            return arrowCount;
        }
        set
        {
            arrowCount += value;
        }
    }
    public void Freeze()
    {
        rigid.mass = 250;
    }
    public void FreezeOff()
    {
        rigid.mass = 0.0001f;
    }
    public void AtkImage(Sprite i)
    {
        AttackBtn.image.sprite = i;
    }

    public void RepMnMe()
    {
        manager.BossMenu();
    }
    public void BreDoor()
    {
        door.layer = 10;
    }
}



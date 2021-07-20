using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Boss : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] PlayerMove player;
    public Light2D light;
    [SerializeField] Animator ani;
    [SerializeField] Collider2D col;
    [SerializeField] Vector2 limitMin, limitMax;
    [SerializeField] ShortAtk satk;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip[] clip;
    [SerializeField] BGMMng B;
    [SerializeField] GameObject Noise;
    [SerializeField] Sprite noise2, bookImage, ghostImage;
    [SerializeField] SoundManager sound;
    [SerializeField] Image BookPan, soundHandle;
    [SerializeField] Button[] moveBtn;
    [SerializeField] GameObject[] oBj;
    public ParticleSystem blood;

    [Header("공격력")]
    [SerializeField] int str = 40;
    [Header("근접공격 데미지")]
    [SerializeField] int str1 = 65;
    [SerializeField] private int hp=15000;  //화살에 100번 맞으면 다 깎이는 체력
    [SerializeField] float speed = 3.3f;
    [SerializeField] float distance = 6.3f;

    Vector3 pPos;
    float v;
    public short startNum = 0; 
    float ranT;  //랜덤한 시간마다 이동
    float ranX, ranY;  //랜덤한 X,Y방향으로 이동
    bool isChase = false; //플레이어를 쫓는 중인가?
    bool isSecPatn = false; //두번째 패턴 시작?
    bool isThiPatn = false; //세번째 패턴 시작?
    bool isFotPat = false; //네번째 패턴 시작?
    

    private void Start()
    {
        v = speed;
        satk.Str1 = str1;
        StartCoroutine(InitPattern());
        InvokeRepeating("RanStop", 0, ranT);
        InvokeRepeating("RanCha", 0.2f, ranT + 2.5f);
        StartCoroutine(BossPattern());
        StartCoroutine(BossPattern2());
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (startNum == 2)
            {
                Limit();
                ani.SetBool("bm", true);
                hpSlider.value = (float)hp / 15000.0f;
              
                if (!isChase)
                {
                    transform.Translate(new Vector2(ranX, ranY).normalized * speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed + 0.5f) * Time.deltaTime);
                }
                
                if(hp<=10500)
                {
                    if(!isSecPatn)
                    {
                        isSecPatn = true;
                        StartCoroutine(Glich());
                        StartCoroutine(BossPattern3());
                    }
                    if(Vector2.Distance(transform.position,player.transform.position)<distance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed + 0.5f) * Time.deltaTime);
                    }
                    if(hp<=6000)
                    {
                        if(!isThiPatn)
                        {
                            isThiPatn = true;
                            light.pointLightInnerRadius = 5;
                            light.pointLightOuterRadius = 8;
                            GameManager.qCount = 14;
                            BookPan.sprite = bookImage;
                            soundHandle.sprite = ghostImage;
                            player.RepMnMe();

                            Noise.SetActive(true);
                            Invoke("NoiseOff", 0.5f);
                            sound.Sound1();
                            player.AtkImage(ghostImage);
                            for(int i=0; i<7; i++)
                            {
                                moveBtn[i].image.sprite = ghostImage;
                            }
                            oBj[0].SetActive(false); oBj[1].SetActive(false); oBj[2].SetActive(false);
                        }
                        if(hp<=1500)
                        {
                            if(!isFotPat)
                            {
                                isFotPat = true;
                                light.color = Color.red;
                                B.PitUp(true);
                                oBj[3].SetActive(true);
                                StartCoroutine(BossPattern4());
                            }
                        }
                    }
                }
                if(hp<=0)
                {
                    player.HpManage = 150;
                    ani.SetTrigger("die");
                    oBj[3].SetActive(false);
                    light.color = Color.white;
                    B.PitUp(false);
                    player.BreDoor();
                    hpSlider.gameObject.SetActive(false);
                    Invoke("Die", 0.2f);
                }
            }
            else if (startNum == 1)
            {
                ani.SetBool("bm", true);
            }
          
        }
        audio.volume = B.slider.value;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
  
    public void SliderOn()
    {
        GameObject _Slider = hpSlider.gameObject;
        _Slider.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="stone")
        {
            BulletManager b = collision.gameObject.GetComponent<BulletManager>();
            hp -= b.SendPower();
            if (hp <= 1500)
                blood.Play();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(startNum==2)
               player.Damaged(str);
        }
    }

    public void Damaged(int d)
    {
        hp -= d;
    }
    public int GetHp()
    {
        return hp;
    }

    private IEnumerator InitPattern()
    {
        ranT = Random.Range(1f, 3.5f);
        ranX = Random.Range(-1f, 1f);
        ranY = Random.Range(-1f, 1f);
        while (true)
        {
            yield return new WaitForSeconds(ranT);
            ranT = Random.Range(1f, 3.5f);
            ranX = Random.Range(-1f, 1f);
            ranY = Random.Range(-1f, 1f);
        }
    }
    void RanStop()  //랜덤한 시간마다 스탑
    {
        int s = Random.Range(1, 9);
        if(s==3)
        {
            speed = 0;
            Invoke("Vs", ranT - 0.3f);
        }
    }
    void Vs()  //스피드 0을 다시 원래 속도로 맞춤
    {
        speed = v;
    }

    void RanCha()  //랜덤한 시간마다 플레이어 추격
    {
        int s = Random.Range(1, 6);
        if(s==2)
        {
            isChase = true;
            Invoke("CC", ranT - 0.4f);
        }
    }
    private void CC()
    {
        isChase = false;
    }
    private IEnumerator BossPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(8.312f);
            if (GameManager.proCount == 10)
            {
                if (GameManager.qCount == 13)
                {
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
                    audio.clip = clip[0];
                    for (int i = 0; i <= 60; i += 30)
                    {
                        GameObject clone = PoolManager.instance.GetQueue3();
                        clone.transform.position = transform.position;
                        clone.transform.rotation = Quaternion.Euler(0, 0, rotateDegree + 180 + i);
                        audio.Play();
                    }
                }
                else if(GameManager.qCount==14)
                {
                    audio.clip = clip[0];
                    for (int i = 0; i <= 360; i += 30)
                    {
                        GameObject clone = PoolManager.instance.GetQueue3();
                        clone.transform.position = transform.position;
                        clone.transform.rotation = Quaternion.Euler(0, 0, i);
                        audio.Play();
                    }
                }
            }
        }
    }

    IEnumerator BossPattern2()
    {
        while(true)
        {
            float r = Random.Range(3f, 6f);
            yield return new WaitForSeconds(r);
            if(GameManager.proCount==10)
            {
                audio.clip = clip[1];
                audio.Play();
                ani.SetTrigger("batk");
            }
        }
    }

    void Limit()
    {
        float x = Mathf.Clamp(transform.position.x, limitMin.x, limitMax.x);
        float y = Mathf.Clamp(transform.position.y, limitMin.y, limitMax.y);
        transform.position = new Vector2(x, y);
    }    

    IEnumerator Glich()
    {
        SpriteRenderer sp = Noise.GetComponent<SpriteRenderer>();
        sp.sprite = noise2;
        while (true)
        {
            yield return new WaitForSeconds(10);
            Noise.SetActive(true);
            Invoke("NoiseOff", 0.5f);
            sound.Sound1();
        }
    }
    void NoiseOff()
    {
        Noise.SetActive(false);
    }
    IEnumerator BossPattern3()
    {
        while(true)
        {
            int a = Random.Range(0, 2);
            float t = Random.Range(7f, 11.5f);
            yield return new WaitForSeconds(t);
            if(a==0)
            {
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
                audio.clip = clip[0];
                for (int i = 0; i <= 360; i += 30)
                {
                    GameObject clone = PoolManager.instance.GetQueue3();
                    clone.transform.position = transform.position;
                    clone.transform.rotation = Quaternion.Euler(0, 0, rotateDegree + 180 + i);
                    audio.Play();
                    yield return new WaitForSeconds(0.15f);
                }
            }
            else
            {
                transform.position = new Vector3(0, 0, -50);
                pPos = player.transform.position;
                yield return new WaitForSeconds(3.5f);
                transform.position = pPos;

                audio.clip = clip[1];
                audio.Play();
                ani.SetTrigger("batk");
            }
        }
    }
   IEnumerator BossPattern4()
    {
        while(true)
        {
            yield return new WaitForSeconds(20f);

            ani.SetTrigger("crazy");
        }
    }
}

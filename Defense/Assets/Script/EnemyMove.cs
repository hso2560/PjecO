using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    protected UpgradeMng upgradeMng;
    protected int wayPointCount;
    protected Transform[] wayPoints;
    protected int currentIndex = 0;
    protected SpriteRenderer Sp;
    protected Rigidbody2D rigid;
    protected Collider2D col;
    public Vector3 dirVec;
    protected GameManager gameManager;
    protected Animator animator;
    [Header("적 속력")] [SerializeField] protected float speed = 5f;
    protected Color oricol;
    [Header("적 최대 HP")][SerializeField] protected int startHp = 100;
    protected int hp;
    [SerializeField] int hp2 = 250;  //3 4 5웨이브 때의 적 체력
    protected PlayerSlider playerSlider;
    [Header("적 드롭 골드")][SerializeField] protected int gold = 50;
    
    int FireDamage;
    int WindDamage;
    int Damage4;
     int Damage5;
    bool isDamaged = false;
    bool isReHp = false;
    [Header("적이 주는 데미지")] [SerializeField] int harm = 50;
    float anotherSpeed;

    public int ShowHp()
    {
        return hp;
    }
    public int ShowFirstHp()
    {
        return startHp;
    }

    protected void Start()
    {
        upgradeMng = FindObjectOfType<UpgradeMng>();
        Sp = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        playerSlider = FindObjectOfType<PlayerSlider>();
        oricol = this.Sp.color;
        hp =startHp ;
        anotherSpeed = speed;
        
    }

    public void Setup(Transform[] wayPoints)
    {
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        transform.position = wayPoints[currentIndex].position;
        StartCoroutine("OnMove");
    }

    protected IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            //transform.Rotate(Vector3.forward * 10);             //회전시키는 코드
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * speed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    protected void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            dirVec = direction;
            
        }
        else
        {
            playerSlider.Damage(harm);
            currentIndex = 0;
            Despawn();
        }
    }

    protected void Update()
    {
        if (Time.timeScale > 0)
        {
           
            if (transform.position.x < wayPoints[currentIndex].position.x)
            {
                animator.Play("EnemyRight");
            }
            if (transform.position.x > wayPoints[currentIndex].position.x)
            {
                animator.Play("EnemyLeft");
            }
            if (transform.position.y < wayPoints[currentIndex].position.y)
            {
                animator.Play("EnemyUp");
            }
            if (transform.position.y > wayPoints[currentIndex].position.y)
            {
                animator.Play("EnemyDown");
            }
            transform.position += dirVec * speed * Time.deltaTime;
        }
        if(hp<=0)
        {
            currentIndex = 0;
            gameManager.GetGold(gold);
            Despawn();
        }
        if(gameManager.wave>=3)
        {
            Sp.color = new Color(0.3f, 0.3f, 0.3f, 0.9f);
            if(!isReHp)
            {
                ReHp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Bullet")
        {
            BulletMove bullet = collision.gameObject.GetComponent<BulletMove>();
            hp -= bullet.damage;
        }
        else if(collision.tag=="Bullet2")
        {
            BulletMove2 bullet = collision.gameObject.GetComponent<BulletMove2>();
            FireDamage = bullet.damage;
            Invoke("constFire", 0f);
            Invoke("constFire", 1f);
            Invoke("constFire", 2f);
            Invoke("constFire", 3f);
        }
        else if(collision.tag=="Bullet4")
        {
            BulletMove4 bullet = collision.gameObject.GetComponent<BulletMove4>();
            Damage4 = bullet.damage;
            StartCoroutine(LBullet());
        }
        else if (collision.tag == "Bullet5")
        {
            Bullet5 bullet = collision.gameObject.GetComponent<Bullet5>();
            Damage5 = bullet.damage;
            StartCoroutine(TBullet());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Bullet3")
        {
            Bullet3 bullet= collision.gameObject.GetComponent<Bullet3>();
            WindDamage = bullet.damage;
            if (isDamaged)
                return;
            isDamaged = true;
            StartCoroutine(windD());
        }
    }

    public void Despawn()
    {
        speed = anotherSpeed;
        gameManager.PlusCount();
        hp = startHp;
        CancelInvoke("constFire");
        PoolManager.instance.InsertQueue(gameObject);
    }

   IEnumerator windD()
    {
        hp -= WindDamage;
        yield return new WaitForSeconds(0.2f);
        isDamaged = false;
    }
    IEnumerator LBullet()  //적이 피해를 받고 잠시 멈춤
    {
        hp -= Damage4;
        speed = 0;
        yield return new WaitForSeconds(2.5f);
        speed = anotherSpeed;
    }
    IEnumerator TBullet()   //적이 피해를 받고 잠시 멈춤
    {
        hp -= Damage5;
        speed = 0;
        yield return new WaitForSeconds(2.5f);
        speed = anotherSpeed;
    }

    void constFire()
    {
        hp -= FireDamage;
    }
    public void ReHp()
    {
        isReHp = true;
        startHp = hp2;
        hp = startHp;
    }
}

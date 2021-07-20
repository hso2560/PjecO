using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    protected PlayerMove player;
    [SerializeField] protected int touchPower=60;
    
    public bool isMove = true;
    protected bool isTarget = false;
    protected Animator ani;
    protected int r;
    
    [SerializeField] protected float speed = 3.8f;
    protected float v;
    [SerializeField] protected float dist = 8f;
    [SerializeField] protected int hp = 800;
    protected SpriteRenderer sp;
    protected Color icolor;

    protected void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        icolor = this.sp.color;
        ani = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMove>();
        v = speed;
        InvokeRepeating("AutoMove", 0, 2f);
    }

    protected void Update()
    {
        if (Time.timeScale > 0)
        {
            if (GameManager.proCount >= 2)  //2
            {
                if (GameManager.qCount >= 6)  //6
                {
                    if (Vector2.Distance(transform.position, player.transform.position) < dist)  
                    {
                       // isTarget = true;
                        //CancelInvoke("AutoMove");
                        ani.Play("EnemyWalk");
                        dirChan();
                        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                    }
                    else
                    {
                        
                        /*if(isTarget)
                        {
                            isTarget = false;
                            InvokeRepeating("AutoMove", 0, 2);
                        }*/

                        if(r==0)
                        {
                            speed = v;
                            ani.Play("EnemyWalk");
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                            transform.Translate(Vector2.down * speed * Time.deltaTime);
                        }
                        else if(r==1)
                        {
                            speed = v;
                            ani.Play("EnemyWalk");
                            transform.rotation = Quaternion.Euler(0, 0, -90);
                            transform.Translate(Vector2.down * speed * Time.deltaTime);
                        }
                        else if(r==2)
                        {
                            speed = v;
                            ani.Play("EnemyWalk");
                            transform.rotation = Quaternion.Euler(0, 0, 180);
                            transform.Translate(Vector2.down * speed * Time.deltaTime);
                        }
                        else if(r==3)
                        {
                            speed = v;
                            ani.Play("EnemyWalk");
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector2.down * speed * Time.deltaTime);
                        }
                        else
                        {
                            speed = 0;
                            ani.Play("EnemyIdle");
                        }
                    }
                }
            }
            else
            {
                ani.Play("EnemyIdle");
            }
        }
    }
    protected void AutoMove()
    {
        r = Random.Range(0, 5);
       
    }

    protected void dirChan()
    {
        float dx = player.transform.position.x - transform.position.x;
        float dy = player.transform.position.y - transform.position.y;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateDegree+90);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Damaged(touchPower);
        }
    }

    public void Damaged(int damage)
    {
        hp -= damage;
        StartCoroutine(damageEffect());
    }

    protected IEnumerator damageEffect()
    {
        sp.color = new Color(0.8f, 1, 1);
        yield return new WaitForSeconds(0.3f);
        sp.color = icolor;
    }
}

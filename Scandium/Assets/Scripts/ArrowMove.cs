using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private int force = 150;

    PlayerMove player;
    
    [SerializeField] float range = 20f;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        LimitDespawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag!="Player")
        {
           
            if (collision.tag == "Enemy1")
            {
                RealEnemy enemy = collision.GetComponent<RealEnemy>();
                enemy.Damaged(force);
            }

            else if (collision.tag == "Obstacle")  //메인 10에서 맵에 배치된 총과의 충돌
            {
                Gun gun = collision.GetComponent<Gun>();

                if (gun.atk)
                {
                    gun.hp--;
                    gun.StartHpEffe();

                    if (gun.hp == 0)
                    {
                        gun.StopShot();
                    }
                }
            }
            else if(collision.tag=="Boss")
            {
                Boss boss = collision.GetComponent<Boss>();
                boss.Damaged(force);
                if(boss.GetHp()<=1500)
                   boss.blood.Play();
            }
            PoolManager.instance.InsertQueue(gameObject);
        }
    }

    private void LimitDespawn()
    {
        if(transform.position.x<player.transform.position.x-range)
        {
            PoolManager.instance.InsertQueue(gameObject);
        }
        else if (transform.position.y < player.transform.position.y-range)
        {
            PoolManager.instance.InsertQueue(gameObject);
        }
        else if (transform.position.x > player.transform.position.x+range)
        {
            PoolManager.instance.InsertQueue(gameObject);
        }
        else if (transform.position.y > player.transform.position.y+range)
        {
            PoolManager.instance.InsertQueue(gameObject);
        }
    }

}

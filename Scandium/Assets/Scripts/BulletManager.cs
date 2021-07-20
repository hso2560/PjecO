using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour   //이름을 BulletMove라고 지으려다가 잘못지음.
{
    [SerializeField] float spinSpeed = -300;
    [SerializeField] int power = 35;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] Vector2 limitMin, limitMax;

    public short bulletId;

    Vector2 bossPos;
    bool isCalc = false;

    private void Start()
    {
        if(bulletId==2)
        {
            power = 500;
            moveSpeed = 18f;
            Destroy(gameObject, 0.8f);
        }
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (GameManager.proCount == 10)
            {
                if (bulletId == 1)
                {
                    transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);

                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                }
                else if (bulletId == 2)
                {
                    GameObject boss = GameObject.FindGameObjectWithTag("Boss"); 
                    transform.position = Vector2.MoveTowards(transform.position, boss.transform.position, moveSpeed * Time.deltaTime);
                }
                else if(bulletId==3)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (!isCalc)
                    {
                        bossPos = player.transform.position;
                        isCalc = true;
                    }
                    transform.Translate(bossPos.normalized * moveSpeed * Time.deltaTime);
                    Limit();
                }
            }
        }
    }

    void Limit()
    {
        if (transform.position.x > limitMax.x)
        {
            PoolManager.instance.InsertQueue3(gameObject);
        }
        else if (transform.position.x < limitMin.x)
        {
            PoolManager.instance.InsertQueue3(gameObject);
        }
        else if (transform.position.y > limitMax.y)
        {
            PoolManager.instance.InsertQueue3(gameObject);
        }
        else if (transform.position.y < limitMin.y)
        {
            PoolManager.instance.InsertQueue3(gameObject);
        }
    }

    public int SendPower() { return power; }
}

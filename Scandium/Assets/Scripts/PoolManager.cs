using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public Queue<GameObject> queue = new Queue<GameObject>();
    [SerializeField] private GameObject Arrow;  //화살

    public Queue<GameObject> queue2 = new Queue<GameObject>();
    [SerializeField] private GameObject Bullet;  // 총알

    public Queue<GameObject> queue3 = new Queue<GameObject>();
    [SerializeField] private GameObject bossBullet; //보스총알

    private void Awake()
    {
        instance = this;

        for(int i=0; i<8; i++)
        {
            GameObject t_arrow = Instantiate(Arrow, Vector2.zero, Quaternion.identity);
            queue.Enqueue(t_arrow);
            t_arrow.SetActive(false);
        }

        Invoke("BulletProd", 1.5f);
    }

    void BulletProd()
    {
        if (GameManager.proCount == 9)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject t_bullet = Instantiate(Bullet, Vector2.zero, Quaternion.identity);
                queue2.Enqueue(t_bullet);
                t_bullet.SetActive(false);
            }
            for(int j=0; j<22; j++)
            {
                GameObject k_bullet = Instantiate(bossBullet, Vector2.zero, Quaternion.identity);
                queue3.Enqueue(k_bullet);
                k_bullet.SetActive(false);
            }
        }
    }

    public void InsertQueue(GameObject p_arrow)
    {
        queue.Enqueue(p_arrow);
        p_arrow.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject arrow = queue.Dequeue();
        arrow.SetActive(true);
        return arrow;
    }

    public void InsertQueue2(GameObject p_bullet)
    {
        queue2.Enqueue(p_bullet);
        p_bullet.SetActive(false);
    }

    public GameObject GetQueue2()
    {
        GameObject bullet = queue2.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void InsertQueue3(GameObject p_bullet)
    {
        queue3.Enqueue(p_bullet);
        p_bullet.SetActive(false);
    }

    public GameObject GetQueue3()
    {
        GameObject bullet = queue3.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }
}

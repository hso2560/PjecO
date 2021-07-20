using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool instance;
    [SerializeField] GameObject bullet = null;
    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i <20; i++)
        {
            GameObject t_enemy = Instantiate(bullet, new Vector2(0, 0), Quaternion.identity);
            queue.Enqueue(t_enemy);
            t_enemy.SetActive(false);
        }
    }

    public void InsertQueue(GameObject p_bullet)
    {
        queue.Enqueue(p_bullet);
        p_bullet.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject t_bullet = queue.Dequeue();
        t_bullet.SetActive(true);
        return t_bullet;
    }
}

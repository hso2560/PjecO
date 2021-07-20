using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager2 : MonoBehaviour
{
    public static PoolManager2 instance;
    [SerializeField] GameObject bullet = null;
    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 20; i++)
        {
            GameObject t_enemy = Instantiate(bullet, new Vector2(0,0 ), Quaternion.identity);
            queue.Enqueue(t_enemy);
            t_enemy.SetActive(false);
        }
      
    }

    public void InsertQueue(GameObject p_enemy)
    {
        queue.Enqueue(p_enemy);
        p_enemy.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject t_enemy = queue.Dequeue();
        t_enemy.SetActive(true);
        return t_enemy;
    }
}

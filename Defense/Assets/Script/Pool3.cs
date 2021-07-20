using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool3 : MonoBehaviour
{
    public static Pool3 instance;
    [SerializeField] GameObject enemy = null;
    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 40; i++)
        {
            GameObject t_enemy = Instantiate(enemy, new Vector2(-10.5f, -1.5f), Quaternion.identity);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool4 : MonoBehaviour
{
    public static Pool4 instance;

    [SerializeField] GameObject enemy = null;
    [SerializeField] GameObject enemy2, enemy3;

    public Queue<GameObject> queue = new Queue<GameObject>();
    public Queue<GameObject> queue2 = new Queue<GameObject>();
    public Queue<GameObject> queue3 = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 40; i++)
        {
            GameObject t_enemy = Instantiate(enemy, new Vector2(-10.5f, -1.5f), Quaternion.identity);
            queue.Enqueue(t_enemy);
            t_enemy.SetActive(false);

            GameObject t_enemy2 = Instantiate(enemy2, new Vector2(-10.5f, -1.5f), Quaternion.identity);
            queue2.Enqueue(t_enemy2);
            t_enemy2.SetActive(false);

            GameObject t_enemy3 = Instantiate(enemy3, new Vector2(-10.5f, -1.5f), Quaternion.identity);
            queue3.Enqueue(t_enemy3);
            t_enemy3.SetActive(false);
        }
    }

    public void InsertQueue(GameObject p_enemy)   //enemy
    {
        queue.Enqueue(p_enemy);
        p_enemy.SetActive(false);
    }

    public GameObject GetQueue()   //enemy
    {
        GameObject t_enemy = queue.Dequeue();
        t_enemy.SetActive(true);
        return t_enemy;
    }

    public void InsertQueue2(GameObject p_enemy)   //enemy2
    {
        queue2.Enqueue(p_enemy);
        p_enemy.SetActive(false);
    }

    public GameObject GetQueue2()
    {
        GameObject t_enemy = queue2.Dequeue();   //enemy2
        t_enemy.SetActive(true);
        return t_enemy;
    }

    public void InsertQueue3(GameObject p_enemy)  //enemy3
    {
        queue3.Enqueue(p_enemy);
        p_enemy.SetActive(false);
    }

    public GameObject GetQueue3()   //enemy3
    {
        GameObject t_enemy = queue3.Dequeue();
        t_enemy.SetActive(true);
        return t_enemy;
    }
}

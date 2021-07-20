using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove4 : MonoBehaviour
{
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected Vector2 limitMin;
    [SerializeField] protected Vector2 limitMax;
    public int damage;

   



    protected void Awake()
    {
       
        Invoke("Despawn", 2f);

    }


    protected void Update()
    {
        ApplyLimit();
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }


    public void ApplyLimit()
    {
        if (transform.position.x > limitMax.x)
        {
            Despawn();
        }
        else if (transform.position.x < limitMin.x)
        {
            Despawn();
        }
        else if (transform.position.y > limitMax.y)
        {
            Despawn();
        }
        else if (transform.position.y < limitMin.y)
        {
            Despawn();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        PoolManager3.instance.InsertQueue(gameObject);
    }
}

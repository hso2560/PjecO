using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove2 : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] Vector2 limitMin;
    [SerializeField] Vector2 limitMax;
    public int damage;
    



    void Awake()
    {
        
        Invoke("Despawn", 2f);
    }


    void Update()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            Despawn();
        }
    }
    public void Despawn()
    {
        
        PoolManager2.instance.InsertQueue(gameObject);
    }
}

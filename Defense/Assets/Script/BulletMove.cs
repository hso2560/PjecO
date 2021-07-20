using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected Vector2 limitMin;
    [SerializeField] protected Vector2 limitMax;
    public int damage;
   // public Vector3 targetPosition;

   // protected PoolManager poolManager;



    protected void Awake()
    {
       // poolManager = FindObjectOfType<PoolManager>();
        Invoke("Despawn", 2f);
      
    }


    protected void Update()
    {
        ApplyLimit();
        transform.Translate(Vector2.right * speed * Time.deltaTime);  //down으로 하면 총알 스프라이트의 방향은 제대로 되지만 적 조준이 엉망이 되버림.
        
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
        if(collision.tag=="Enemy")
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        /* transform.SetParent(poolManager.transform, true);
         gameObject.SetActive(false);*/
        Pool.instance.InsertQueue(gameObject);
    }
  
}

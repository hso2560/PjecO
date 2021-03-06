using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int damage = 5;
    public bool isEnemy = false;

    Rigidbody2D rigid;
    public Vector2 dir;

    private int ownerId;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.velocity = dir * moveSpeed;
    }

    public void ResetData(int owner, Vector3 position, Vector2 dir, float speed, int damage, bool isEnemy)
    {
        ownerId = owner;
        transform.position = position;
        this.dir = dir.normalized;
        moveSpeed = speed;
        this.damage = damage;
        this.isEnemy = isEnemy;

        gameObject.layer = isEnemy ? GameManager.EnemyLayer : GameManager.PlayerLayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable id = collision.GetComponent<IDamageable>();

        if (id != null)
        {
            id.OnDamage(damage, dir, isEnemy, ownerId);
        }
        Explosion exp = EffectManager.GetExplosion();
        exp.ResetPos(transform.position);

        gameObject.SetActive(false);
    }
}

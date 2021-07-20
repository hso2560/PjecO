using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower4 : MonoBehaviour
{
    protected string tag = "Enemy";
    protected Transform target;
    public int Lv = 1;
    public int inc = 0;
    public int Damage = 80;
    public int gold = 0;
    [SerializeField] protected float distan = 3f;
    UpgradeMng upgradeMng;
    public Tile tile;
    protected Transform enemyTransform;
    protected Rigidbody2D rigid;
    [SerializeField] protected float delay = 0.4f;

    protected float dist;
    [SerializeField] protected float distance = 3f;
    AudioSource audio;
   
    protected void Start()
    {
        //DontDestroyOnLoad(gameObject);

        InvokeRepeating("getClosestEnemy", 0, delay);
        upgradeMng = FindObjectOfType<UpgradeMng>();
        audio = GetComponent<AudioSource>();
    }

    protected void Update()
    {
        if (SoundMng.isEffectS)
            audio.volume = 0.5f;
        else
            audio.volume = 0;
    }
    protected void getClosestEnemy()
    {
        GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag(tag);
        float closestDistSpr = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (GameObject taggedEnemy in taggedEnemys)
        {
            Vector3 objectPos = taggedEnemy.transform.position;
            dist = (objectPos - transform.position).sqrMagnitude;
            if (dist <= distan)
            {
                if (dist < closestDistSpr)
                {
                    closestDistSpr = dist;
                    closestEnemy = taggedEnemy.transform;

                }
            }
        }
        target = closestEnemy;
        if (target != null)
        {
            GameObject clone = PoolManager3.instance.GetQueue();
            BulletMove4 bullet = clone.GetComponent<BulletMove4>();
            bullet.damage = Damage+inc;
            clone.transform.position = transform.position;
           
            audio.Play();
            float dx = target.position.x - transform.position.x;
            float dy = target.position.y - transform.position.y;
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            clone.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);

        }
    }
    public void TextSend()
    {
        upgradeMng.Lv.text = string.Format("레벨: {0}", Lv);
        upgradeMng.speed.text = string.Format("공격속도: {0}", delay);
        upgradeMng.Feature.text = "<color=#FFFC2A>특성: 2.5초간 적을 멈춤</color>";
    }

}

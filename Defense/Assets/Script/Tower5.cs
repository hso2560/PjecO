using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower5 : MonoBehaviour
{
    protected string tag = "Enemy";
    protected Transform target;
    public int Lv = 1;
    public int inc = 0;
    public int Damage = 200;
    [SerializeField] protected float distan = 3f;
    UpgradeMng upgradeMng;
    public int gold = 0;
    public Tile tile;
    protected Transform enemyTransform;
    protected Rigidbody2D rigid;
    [SerializeField] protected float delay = 0.4f;
    AudioSource audio;

    protected float dist;
    [SerializeField] protected float distance = 3f;
 
    protected void Start()
    {
        upgradeMng = FindObjectOfType<UpgradeMng>();
        //  DontDestroyOnLoad(gameObject);
        audio = GetComponent<AudioSource>();
        InvokeRepeating("getClosestEnemy", 0, delay);
       
    }
    protected void Update()
    {
        if (SoundMng.isEffectS)
            audio.volume = 0.8f;
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
            GameObject clone = PoolManager4.instance.GetQueue();
            Bullet5 bullet = clone.GetComponent<Bullet5>();
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
        upgradeMng.Feature.text = "<color=#10AE00>특성: 2.5초간 적에게 강한 데미지를 주고 멈춤</color>";
    }
}

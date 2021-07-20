using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower2 : MonoBehaviour
{
    string tag = "Enemy";
    Transform target;
    protected UpgradeMng upgradeMng;
    public int Lv = 1;
    [SerializeField] float distan=3f;
    public int Damage = 20;
    public int inc = 0;
    public int gold = 0;
    [SerializeField] float delay = 0.6f;
    public Tile tile;
    float dist;
    [SerializeField] float distance = 3f;

    AudioSource audio;
 
    void Start()
    {
       // DontDestroyOnLoad(gameObject);
        upgradeMng = FindObjectOfType<UpgradeMng>();
        InvokeRepeating("getClosestEnemy", 0, delay);
        audio = GetComponent<AudioSource>();
    }
    protected void Update()
    {
        if (SoundMng.isEffectS)
            audio.volume = 0.5f;
        else
            audio.volume = 0;
    }

    void getClosestEnemy()
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
            GameObject clone = PoolManager2.instance.GetQueue();
            BulletMove2 bullet = clone.GetComponent<BulletMove2>();
            bullet.damage = Damage+inc;
            clone.transform.position = transform.position;
           
            audio.Play();
            float dx = target.position.x - transform.position.x;
            float dy = target.position.y - transform.position.y;
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            clone.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
            /*float dx = target.position.x - transform.position.x;
            float dy = target.position.y - transform.position.y;
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            Transform bulletTransform = null;
            GameObject bulletGO = null;

            if (poolManager.transform.childCount > 0)
            {
                bulletTransform = poolManager.transform.GetChild(0);
                bulletGO = bulletTransform.gameObject;
            }
            else
            {

                bulletGO = Instantiate(bullet, bulletPosition.position, Quaternion.Euler(0, 0, rotateDegree));
                bulletTransform = bulletGO.transform;
              
            }
            bulletGO.SetActive(true);
            bulletTransform.position = bulletPosition.position;
            bulletTransform.SetParent(null, true);*/



        }
    }


    public void TextSend()
    {
        upgradeMng.Lv.text = string.Format("레벨: {0}", Lv);
        upgradeMng.speed.text = string.Format("공격속도: {0}", delay);
        upgradeMng.Feature.text = "<color=#EC0000>특성: 처음공격을 포함해서 1초마다 "+(Damage+inc).ToString()+"만큼의 데미지를 3초간 준다</color>";
    }
}

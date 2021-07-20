using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower1 : MonoBehaviour
{
    protected UpgradeMng upgradeMng;
    protected string tag = "Enemy";
    protected Transform target;
    public int Lv = 1;
    public int gold = 0;  //타워 레벨이 높아질수록 타워 판매 금액 증가   gold==0인것은 타워 레벨이 1레벨
    public int Damage = 50;
    [SerializeField] protected float distan = 3f;
    public int inc = 0;
    protected Transform enemyTransform;
    protected Rigidbody2D rigid;
    [SerializeField] protected float delay = 0.4f;
    
     protected float dist;
    [SerializeField] protected float distance = 3f;

    public Tile tile;
    AudioSource audio;
    

    protected void Start()
    { 
        //DontDestroyOnLoad(gameObject);
        InvokeRepeating("getClosestEnemy", 0, delay);
        upgradeMng = FindObjectOfType<UpgradeMng>();
        audio = GetComponent<AudioSource>();
       
    }

    private void Update()
    {
        if (SoundMng.isEffectS)
            audio.volume = 1;
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
            GameObject clone = Pool.instance.GetQueue();
            BulletMove bullet = clone.GetComponent<BulletMove>();
            bullet.damage = Damage+inc;
            clone.transform.position = transform.position;
           
            audio.Play();
            float dx = target.position.x - transform.position.x;
            float dy = target.position.y - transform.position.y;
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            clone.transform.rotation = Quaternion.Euler(0, 0, rotateDegree);
            /* float dx = target.position.x - bulletPosition.position.x; 
             float dy = target.position.y - bulletPosition.position.y;
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

                     bulletGO = Instantiate(bullet, bulletPosition.position, Quaternion.Euler(0,0,rotateDegree));
                     bulletTransform = bulletGO.transform;
                     //bulletGO.transform.Rotate(new Vector3(0, 0, rotateDegree));
                 }
             bulletGO.SetActive(true);
             bulletTransform.position = bulletPosition.position;
             bulletTransform.SetParent(null, true);
*/
        }
    }
    public void TextSend()
    {
        upgradeMng.Lv.text = string.Format("레벨: {0}", Lv);
        upgradeMng.speed.text = string.Format("공격속도: {0}", delay);
        upgradeMng.Feature.text = "<color=#001DE5>특성: 밸런스적인 기본 원소</color>";
    }
   
}

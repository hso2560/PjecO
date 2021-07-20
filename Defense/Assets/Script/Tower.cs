using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bullet = null;
    [SerializeField] Transform bulletPosition;
    PoolManager poolManager;
    private List<GameObject> collEnemys = new List<GameObject>();
    private float fTime = 0;


    private void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
    }

    void Update()
    {
        fTime += Time.deltaTime;
        if (collEnemys.Count > 0)
        {
            GameObject target = collEnemys[0];
            if (gameObject.tag == "Tower")
            {
                if (target != null && fTime > 0.5f)
                {
                    fTime = 0.0f;
                    
                    Transform bulletTransform = null;
                    GameObject bulletGO = null;
                    
                    if (poolManager.transform.childCount > 0)
                    {
                        bulletTransform = poolManager.transform.GetChild(0);
                        bulletGO = bulletTransform.gameObject;
                    }
                    else
                    {

                        var aBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
                        bulletGO = aBullet;
                        bulletTransform = bulletGO.transform;
                       // bullet.transform.Rotate(new Vector3(0, 0, rotateDegree));
                    }
                    bulletGO.SetActive(true);
                    bulletTransform.position = bulletPosition.position;
                    bulletTransform.SetParent(null, true);
                    // aBullet.GetComponent<BulletMove>().targetPosition = (target.transform.position - transform.position).normalized;
                    //aBullet.transform.localScale = new Vector3(0.5f, 0.5f);
                    Vector3 dir = (target.transform.position - transform.position).normalized;
                    float angle = Vector2.SignedAngle(Vector2.down, dir);
                    Quaternion qut = new Quaternion();
                    qut.eulerAngles = new Vector3(0, 0, angle);
                    bulletGO.transform.rotation = qut;
                    //bulletGO.transform.position += dir * 1.0f;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            collEnemys.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject go in collEnemys)
        {
            if (go == other.gameObject)
            {
                collEnemys.Remove(go);
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    float spawnTime=1f;  //적 생성 쿨타임
    float FirSpawn = 8f;
    [SerializeField]
    Transform[] wayPoints;
    [Header("적의 수")] [SerializeField] int mobSum = 30;
    [Header("건들면 안됨")] [SerializeField] int count = 0;   //몹이 지금까지 총 몇 마리 나왔는지 확인하기 위해서 [SerializeField]선언.
    GameManager manager;
    public GameObject[] dangerObj = new GameObject[2];

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnEnemy());
        manager.PlusSum(mobSum);
        StartCoroutine(DangerSign());
    }
    private IEnumerator SpawnEnemy()
    {
        if (manager.wave < 6)
        {
            yield return new WaitForSeconds(FirSpawn);
            dangerObj[0].SetActive(false);
            dangerObj[1].SetActive(false);
            if (manager.wave >= 5)
            {
                spawnTime = 0.5f;
            }
            while (count < mobSum)
            {
                count++;
                GameObject clone = PoolManager.instance.GetQueue();
                EnemyMove enemy = clone.GetComponent<EnemyMove>();
                enemy.Setup(wayPoints);
                yield return new WaitForSeconds(spawnTime);

            }
        }
        else if(manager.wave<11)
        {
            
            yield return new WaitForSeconds(FirSpawn);
            dangerObj[0].SetActive(false);
            dangerObj[1].SetActive(false);
            spawnTime = 1f;
            if (manager.wave >= 10)
            {
                spawnTime = 0.5f;
            }
            while (count < mobSum)
            {
                count++;
                GameObject clone = Pool2.instance.GetQueue();
                EnemyMove2 enemy = clone.GetComponent<EnemyMove2>();
                enemy.Setup(wayPoints);
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else if(manager.wave<16)
        {
            yield return new WaitForSeconds(FirSpawn);
            dangerObj[0].SetActive(false);
            dangerObj[1].SetActive(false);
            spawnTime = 1f;
            if (manager.wave >= 15)
            {
                spawnTime = 0.5f;
            }
            while (count < mobSum)
            {
                count++;
                GameObject clone = Pool3.instance.GetQueue();
                EnemyMove3 enemy = clone.GetComponent<EnemyMove3>();
                enemy.Setup(wayPoints);
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else if (manager.wave < 21)
        {
            yield return new WaitForSeconds(FirSpawn);
            dangerObj[0].SetActive(false);
            dangerObj[1].SetActive(false);
            spawnTime = 1f;
            if (manager.wave >= 20)
            {
                spawnTime = 0.5f;
            }
            while (count < mobSum)
            {
                count++;
                GameObject clone = Pool4.instance.GetQueue();
                EnemyMove4 enemy = clone.GetComponent<EnemyMove4>();
                enemy.Setup(wayPoints);
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else if (manager.wave < 26)
        {
            yield return new WaitForSeconds(FirSpawn);
            dangerObj[0].SetActive(false);
            dangerObj[1].SetActive(false);
            spawnTime = 1f;
            if (manager.wave >= 25)
            {
                spawnTime = 0.5f;
            }
            while (count < mobSum)
            {
                count++;
                GameObject clone = Pool4.instance.GetQueue2();
                EnemyMove5 enemy = clone.GetComponent<EnemyMove5>();
                enemy.Setup(wayPoints);
                yield return new WaitForSeconds(spawnTime);
            }
        }
        else if (manager.wave < 31)
        {
            yield return new WaitForSeconds(FirSpawn);
            dangerObj[0].SetActive(false);
            dangerObj[1].SetActive(false);
            spawnTime = 1f;
            if (manager.wave >= 30)
            {
                spawnTime = 0.5f;
            }
            while (count < mobSum)
            {
                count++;
                GameObject clone = Pool4.instance.GetQueue3();
                EnemyMove6 enemy = clone.GetComponent<EnemyMove6>();
                enemy.Setup(wayPoints);
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
    private IEnumerator DangerSign()
    {
        yield return new WaitForSeconds(5f);
        dangerObj[0].SetActive(true);
        dangerObj[1].SetActive(true);
    }
    public void Rreset(int c)
    {
        mobSum = c;
        count = 0;
    }
    public void ReStart()
    {

        StartCoroutine(SpawnEnemy());
        manager.PlusSum(mobSum);
        StartCoroutine(DangerSign());
    }
}

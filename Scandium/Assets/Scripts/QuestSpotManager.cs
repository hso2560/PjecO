using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSpotManager : MonoBehaviour
{
    private GameManager manager;
    private PlayerMove player;
    private ObjData objData;
    public GameObject BackPerson;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMove>();
        objData = GetComponent<ObjData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.proCount <= 2)
        {
            if (collision.tag == "Player")
            {
                manager.Quest1(gameObject);
            }
        }
        else if(GameManager.proCount==3)
        {
            if(collision.tag=="Player")
            {
                GameManager.proCount = 4;
                SceneManager.LoadScene("Main5");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.proCount == 12)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameObject.layer = 10;
                manager.Quest1(gameObject);
                BackPerson.SetActive(true);
                GameManager.qCount = 16;
            }
        }
    }
    public void DataChange()
    {
        objData.id = 61;
        GameManager.qCount = 3;
        print(GameManager.qCount);
    }
    public void DataChange2()
    {
        objData.id = 66;
    }
}

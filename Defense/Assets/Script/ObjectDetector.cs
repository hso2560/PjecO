using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] TowerSpawner towerSpawner;
    [SerializeField] GameObject TowerRange;
    [SerializeField] GameObject UpPanel;

    UpgradeMng upgradeMng;
   Camera mainCamera;
    RaycastHit hit;
    GameManager manager;

    int price; //타워 회수 비용

    GameObject tower;
    Ray ray;
    Tile tile;

    //타워 판매 가격(1레벨일때)
    [SerializeField] int price1 = 6;
    [SerializeField] int price2 = 9;
    [SerializeField] int price3 = 15;
    [SerializeField] int price4 = 24;
    [SerializeField] int price5 = 24;

    [SerializeField] int[] p = new int[5] {2,3,5,8,10};  //타워 판매 가격 증가
    [SerializeField] int[] upStr = new int[5] { 10, 5, 2, 20, 30 };   //업그레이드 증가 공격력
    [SerializeField] int[] con = new int[5] { 8, 11, 20, 35, 45 };    //업그레이드 비용
    

    void Awake()
    {
        // DontDestroyOnLoad(gameObject);
       mainCamera = Camera.main;
        upgradeMng = FindObjectOfType<UpgradeMng>();
        manager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Tile"))
                {
                    towerSpawner.SpawnTower(hit.transform);
                }
                
                else if(hit.transform.CompareTag("Tower"))
                {
                    TowerRange.SetActive(true);
                    UpPanel.SetActive(true);
                    TowerRange.transform.position = hit.transform.position;
                    
                    if(hit.transform.gameObject.name=="WaterTower(Clone)")
                    {
                        Tower1 tower = hit.transform.gameObject.GetComponent<Tower1>();
                        tile = tower.tile;
                        tower.TextSend();
                        upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage+tower.inc);
                        upgradeMng.upPrice.text = con[0].ToString();
                        
                        price = price1+tower.gold;
                        upgradeMng.sellPrice.text = price.ToString();
                        this.tower = tower.gameObject;
                    }
                    else if (hit.transform.gameObject.name == "FireTower(Clone)")
                    {
                        Tower2 tower = hit.transform.gameObject.GetComponent<Tower2>();
                        tile = tower.tile;
                        tower.TextSend();
                        upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage+tower.inc);
                        upgradeMng.upPrice.text = con[1].ToString();
                       
                        price = price2 + tower.gold;
                        upgradeMng.sellPrice.text = price.ToString();
                        this.tower = tower.gameObject;
                    }
                    else if (hit.transform.gameObject.name == "WindTower(Clone)")
                    {
                        Tower3 tower = hit.transform.gameObject.GetComponent<Tower3>();
                        tile = tower.tile;
                        tower.TextSend();
                        upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage+tower.inc);
                        upgradeMng.upPrice.text = con[2].ToString();
                       
                        price = price3 + tower.gold;
                        upgradeMng.sellPrice.text = price.ToString();
                        this.tower = tower.gameObject;
                    }
                    else if (hit.transform.gameObject.name == "LightningTower(Clone)")
                    {
                        Tower4 tower = hit.transform.gameObject.GetComponent<Tower4>();
                        tile = tower.tile;
                        tower.TextSend();
                        upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage+tower.inc);
                        upgradeMng.upPrice.text = con[3].ToString();
                        
                        price = price4 + tower.gold;
                        upgradeMng.sellPrice.text = price.ToString();
                        this.tower = tower.gameObject;
                    }
                    else if (hit.transform.gameObject.name == "TreeTower(Clone)")
                    {
                        Tower5 tower = hit.transform.gameObject.GetComponent<Tower5>();
                        tile = tower.tile;
                        tower.TextSend();
                        upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage+tower.inc);
                        upgradeMng.upPrice.text = con[4].ToString();
                        
                        price = price5 + tower.gold;
                        upgradeMng.sellPrice.text = price.ToString();
                        this.tower = tower.gameObject;
                    }
                }
            }
        }
       
    }
   
    public void Back()
    {
        TowerRange.SetActive(false);
        UpPanel.SetActive(false);
        tower = null;
    }

    public void Sell()
    {
        tile.IsBuildTower = false;
        manager.GetGold(price);
        Destroy(tower);
        TowerRange.SetActive(false);
        UpPanel.SetActive(false);
    }
    public void ClickUpgrade()
    {
        if (tower.gameObject.name == "WaterTower(Clone)")
        {
            if (manager.GoldManage >= con[0])
            {
                Tower1 tower = this.tower.GetComponent<Tower1>();
                tower.inc += upStr[0];  //물공격 업그레이드 공격력 증가량
                tower.Lv++;  //물 타워 레벨 증가
                tower.gold += p[0];  //타워 회수 금액 증가
                price = price1 + tower.gold;
                upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage + tower.inc);
                tower.TextSend();
                manager.GoldManage = con[0];  //돈 소비
            }
        }
        else if(tower.gameObject.name== "FireTower(Clone)")
        {
            if (manager.GoldManage >= con[1])
            {
                Tower2 tower = this.tower.GetComponent<Tower2>();
                tower.inc += upStr[1];
                tower.Lv++;
                tower.gold += p[1];
                price = price2 + tower.gold;
                upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage + tower.inc);
                tower.TextSend();
                manager.GoldManage = con[1];
            }
        }
        else if(tower.gameObject.name == "WindTower(Clone)")
        {
            if (manager.GoldManage >= con[2])
            {
                Tower3 tower = this.tower.GetComponent<Tower3>();
                tower.inc += upStr[2];
                tower.Lv++;
                tower.gold += p[2];
                price = price3 + tower.gold;
                upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage + tower.inc);
                tower.TextSend();
                manager.GoldManage = con[2];
            }
        }
        else if(tower.gameObject.name== "LightningTower(Clone)")
        {
            if (manager.GoldManage >= con[3])
            {
                Tower4 tower = this.tower.GetComponent<Tower4>();
                tower.inc += upStr[3];
                tower.Lv++;
                tower.gold += p[3];
                price = price4 + tower.gold;
                upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage + tower.inc);
                tower.TextSend();
                manager.GoldManage = con[3];
            }
        }
        else if (tower.gameObject.name == "TreeTower(Clone)")
        {
            if (manager.GoldManage >= con[4])
            {
                Tower5 tower = this.tower.GetComponent<Tower5>();
                tower.inc += upStr[4];
                tower.Lv++;
                tower.gold += p[4];
                price = price5 + tower.gold;
                upgradeMng.Str.text = string.Format("공격력: {0}", tower.Damage + tower.inc);
                tower.TextSend();
                manager.GoldManage = con[4];
            }
        }
    }
}

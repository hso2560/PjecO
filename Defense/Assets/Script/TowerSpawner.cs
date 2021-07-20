using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject towerPrefab2;
    [SerializeField]
    private GameObject towerPrefab3;
    [SerializeField]
    private GameObject towerPrefab4;
    [SerializeField]
    private GameObject towerPrefab5;

    private GameManager manager;

    [Header("타워 소환 소모 포인트")]
    [SerializeField] int towerGold1 = 20;
    [SerializeField] int towerGold2 = 40;
    [SerializeField] int towerGold3 = 80;
    [SerializeField] int towerGold4 = 100;
    [SerializeField] int towerGold5 = 120;

    [SerializeField] Button[] button = new Button[5];

    bool isTower1 = false;
    bool isTower2 = false;
    bool isTower3 = false;
    bool isTower4 = false;
    bool isTower5 = false;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
       
    }

    private void Update()
    {
        TowerButton();
    }

    private void TowerButton()
    {
        if (manager.GoldManage < towerGold1)
        {
            button[0].interactable = false;
        }
        else
        {
            button[0].interactable = true;
        }
        if (manager.GoldManage < towerGold2)
        {
            button[1].interactable = false;
        }
        else
        {
            button[1].interactable = true;
        }

        if (manager.GoldManage < towerGold3)
        {
            button[2].interactable = false;
        }
        else 
        {
            button[2].interactable = true;
        }

        if (manager.GoldManage < towerGold4)
        {
            button[3].interactable = false;
        }
        else
        {
            button[3].interactable = true;
        }
        if (manager.GoldManage < towerGold5)
        {
            button[4].interactable = false;
        }
        else
        {
            button[4].interactable = true;
        }
    }
    

    public void SpawnTower(Transform tileTransform)
    {
        if (isTower1)
        {
            if (manager.GoldManage >= towerGold1)
            {
                Tile tile = tileTransform.GetComponent<Tile>();

                if (tile.IsBuildTower == true)
                {
                    return;
                }
                tile.IsBuildTower = true;

                GameObject obj=Instantiate(towerPrefab, tileTransform.position + Vector3.back + new Vector3(0, 0.3f, 0), Quaternion.identity);
                Tower1 tower = obj.GetComponent<Tower1>();
                tower.tile = tile;
                manager.GoldManage = towerGold1;
                OffTowerButton();
            }
        }
        else if (isTower2)
        {
            if(manager.GoldManage>=towerGold2)
            {
                Tile tile = tileTransform.GetComponent<Tile>();

                if (tile.IsBuildTower == true)
                {
                    return;
                }
                tile.IsBuildTower = true;

                GameObject obj = Instantiate(towerPrefab2, tileTransform.position + Vector3.back + new Vector3(0, 0.3f, 0), Quaternion.identity);
                Tower2 tower = obj.GetComponent<Tower2>();
                tower.tile = tile;
                manager.GoldManage = towerGold2;
                OffTowerButton();
            }
        }
        else if (isTower3)
        {
            if(manager.GoldManage>=towerGold3)
            {
                Tile tile = tileTransform.GetComponent<Tile>();

                if (tile.IsBuildTower == true)
                {
                    return;
                }
                tile.IsBuildTower = true;

                GameObject obj = Instantiate(towerPrefab3, tileTransform.position + Vector3.back + new Vector3(0, 0.3f, 0), Quaternion.identity);
                Tower3 tower = obj.GetComponent<Tower3>();
                tower.tile = tile;
                manager.GoldManage = towerGold3;
                OffTowerButton();
            }
        }
        else if(isTower4)
        {
            if (manager.GoldManage >= towerGold4)
            {
                Tile tile = tileTransform.GetComponent<Tile>();

                if (tile.IsBuildTower == true)
                {
                    return;
                }
                tile.IsBuildTower = true;

                GameObject obj = Instantiate(towerPrefab4, tileTransform.position + Vector3.back + new Vector3(0, 0.3f, 0), Quaternion.identity);
                Tower4 tower = obj.GetComponent<Tower4>();
                tower.tile = tile;
                manager.GoldManage = towerGold4;
                OffTowerButton();
            }
        }
        else if(isTower5)
        {
            if (manager.GoldManage >= towerGold5)
            {
                Tile tile = tileTransform.GetComponent<Tile>();

                if (tile.IsBuildTower == true)
                {
                    return;
                }
                tile.IsBuildTower = true;

                GameObject obj = Instantiate(towerPrefab5, tileTransform.position + Vector3.back + new Vector3(0, 0.3f, 0), Quaternion.identity);
                Tower5 tower = obj.GetComponent<Tower5>();
                tower.tile = tile;
                manager.GoldManage = towerGold5;
                OffTowerButton();
            }
        }
    }

    public void OnClickTower1()
    {
        isTower1 = true;
        isTower2 = false;
        isTower3 = false;
        isTower4 = false;
        isTower5 = false;
    }
    public void OnClickTower2()
    {
        isTower2 = true;
        isTower1 = false;
        isTower3 = false;
        isTower4 = false;
        isTower5 = false;
    }
    public void OnClickTower3()
    {
        isTower2 = false;
        isTower1 = false;
        isTower3 = true;
        isTower4 = false;
        isTower5 = false;
    }
    public void OnClickTower4()
    {
        isTower2 = false;
        isTower1 = false;
        isTower3 = false;
        isTower4 = true;
        isTower5 = false;
    }
    public void OnClickTower5()
    {
        isTower2 = false;
        isTower1 = false;
        isTower3 = false;
        isTower4 = false;
        isTower5 = true;
    }
    public void OffTowerButton()
    {
        isTower2 = false;
        isTower1 = false;
        isTower3 = false;
        isTower4 = false;
        isTower5 = false;
    }
}

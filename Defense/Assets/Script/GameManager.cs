using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold = 5000;  //플레이어 포인트(돈)
    public Text goldTxt;
    public Text WaveTxt;
    public Text Clear;
    public int wave = 1;
    [Header("건들면 안됨")] [SerializeField] private int count = 0;  //몹 상태 확인하기 위해서 [SerializeField]
    [Header("건들면 안됨")] [SerializeField] private int sum;        //몹 총 수 확인하기 위해서 [SerializeField]
    private bool isPause=false;
    [SerializeField] private GameObject pausePanel;
    PlayerSlider player;
    bool isSave = false;
    //public static bool isOff = false;
    EnemySpawn enemySpawn;


    private void Awake()
    {
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        player = GetComponent<PlayerSlider>();
        enemySpawn = FindObjectOfType<EnemySpawn>();
    }


    private void Update()
    {
        goldTxt.text = string.Format("x{0}" ,gold);
        WaveTxt.text = wave.ToString() + "WAVE";
        pausePanel.SetActive(isPause);
        if (wave < 30)  
        {
            if (count == sum)
            {
                count = 0;
                sum = 0;

                if (wave == 1)     //괄호안의 숫자는 2웨이브 때의 몹 수
                    enemySpawn.Rreset(40);
               

                else if (wave == 2)
                    enemySpawn.Rreset(50);    //괄호안의 숫자는 3웨이브 때의 몹 수

                else if (wave == 3)
                    enemySpawn.Rreset(50);

                else if (wave == 4)
                    enemySpawn.Rreset(50);
                else if (wave == 5)
                    enemySpawn.Rreset(30);
                else if (wave == 6)
                    enemySpawn.Rreset(40);
                else if (wave == 7)
                    enemySpawn.Rreset(50);
                else if (wave == 8)
                    enemySpawn.Rreset(50);
                else if (wave == 9)
                    enemySpawn.Rreset(50);   //괄호안의 숫자는 10웨이브 때의 몹 수
                else if (wave == 10)
                    enemySpawn.Rreset(30);
                else if (wave == 11)
                    enemySpawn.Rreset(40);
                else if (wave == 12)
                    enemySpawn.Rreset(50);
                else if (wave == 13)
                    enemySpawn.Rreset(50);
                else if (wave == 14)
                    enemySpawn.Rreset(50);
                else if (wave == 15)
                    enemySpawn.Rreset(30);
                else if (wave == 16)
                    enemySpawn.Rreset(40);
                else if (wave == 17)
                    enemySpawn.Rreset(50);
                else if (wave == 18)
                    enemySpawn.Rreset(50);
                else if (wave == 19)
                    enemySpawn.Rreset(50);
                else if (wave == 20)
                    enemySpawn.Rreset(30);
                else if (wave == 21)
                    enemySpawn.Rreset(40);
                else if (wave == 22)
                    enemySpawn.Rreset(50);
                else if (wave == 23)
                    enemySpawn.Rreset(50);
                else if (wave == 24)
                    enemySpawn.Rreset(50);
                else if (wave == 25)
                    enemySpawn.Rreset(30);
                else if (wave == 26)
                    enemySpawn.Rreset(40);
                else if (wave == 27)
                    enemySpawn.Rreset(50);
                else if (wave == 28)
                    enemySpawn.Rreset(50);
                else if (wave == 29)
                    enemySpawn.Rreset(50);
                wave++;
                enemySpawn.ReStart();
            }
        }
        else if(wave==30)
        {
            if(count==sum)
            {
                Clear.text = "Clear";
                Invoke("clear", 3f);
            }
        }
    }
    
    void clear()
    {
        
       
        Clear.text = "";
        if (!isSave)
            LobbyManager.clearCount++;
        isSave = true;
        PlayerPrefs.SetInt("CLEAR", LobbyManager.clearCount);
        SceneManager.LoadScene("Lobby");
    }

    public void GetGold(int value)
    {
        gold += value;
    }
    public int GoldManage
    {
        get
        {
            return gold;
        }
        set
        {
            gold -= value;
        }
    }
    public void PlusCount()
    {
        count++;
    }
    public void PlusSum(int value)
    {
        sum += value;
    }
    public void OnClickPause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
           
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
        }
    }
    public void OnConti()
    {
        isPause = false;
        Time.timeScale = 1;
    }
    public void OnQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
        OnReset();
    }
    public void OnReset()
    {
        
        player.HpReset();
        gold = 50;
        wave = 1;
    }
}

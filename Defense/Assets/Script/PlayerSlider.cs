using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSlider : MonoBehaviour
{
    [SerializeField] private Text hpTxt;
    [SerializeField] GameObject failTxt;
    GameManager gm;
    [SerializeField] private int hp=20;  //플레이어 체력

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    private void Update()
    {
        hpTxt.text = string.Format("x{0}", hp);
        
        if (hp < 0)
        {
            hp = 0;
        }
        else if (hp == 0)
        {
            failTxt.SetActive(true);
            Invoke("GameOver", 3f);
        }
        
    }

    private void GameOver()
    {
        failTxt.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
        gm.OnReset();
    }
    public void Damage(int value)
    {
        hp -= value;
    }
    public void HpReset()
    {
        hp = 20;
    }
}

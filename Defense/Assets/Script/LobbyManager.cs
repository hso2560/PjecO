using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    GameObject quitPanel;
    public static int clearCount=0;
    [SerializeField] Text clearTxt;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("CLEAR"))
           clearCount = PlayerPrefs.GetInt("CLEAR");
        //AdmobManager.instance.Load_ad();
    }
    private void Update()
    {
        clearTxt.text = string.Format("x{0}", clearCount);
    }

    public void OnClickQuit()
    {
        quitPanel.SetActive(true);
    }
    public void OnClickBack()
    {
        quitPanel.SetActive(false);
    }
    public void Onqu()
    {
        Application.Quit();
    }
   
    public void GoMain()
    {
        Invoke("goMain", 0.5f);   
    }

    public void goMain()
    {
      
        SceneManager.LoadScene("Main0");
    }
    public void TestResetButt()
    {
        clearCount = 0;
        PlayerPrefs.DeleteAll();
    }
}

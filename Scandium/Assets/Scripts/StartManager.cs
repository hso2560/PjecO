using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartManager : MonoBehaviour
{
    [SerializeField] Text startTxt;
    [SerializeField] GameObject HiddenPanel;
    [SerializeField] GameObject QuitPanel;
    Color sColor;
    bool isPress = false;
    
    private void Awake()
    {
        sColor = startTxt.color;
        StartCoroutine(TxtEffect());
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPress)
            {
                isPress = true;
            }
            else
            {
                isPress = false;
            }
            QuitPanel.SetActive(isPress);
        }
    }
    public void OnBack()
    {
        isPress = false;
        QuitPanel.SetActive(isPress);
    }
    public void Yes()
    {
        Application.Quit();
    }

    public void ClickStart()
    {
        sColor.a = 0.5f;
        startTxt.color = sColor;
        Invoke("GoLobby",0.5f);
        
    }

    private void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void ClickHide()
    {
        HiddenPanel.SetActive(true);
    }
    public void ClickBack()
    {
        HiddenPanel.SetActive(false);
    }

    IEnumerator TxtEffect()
    {
        while (true)
        {
            for (int i = 0; i < 50; i++)
            {
                yield return new WaitForSeconds(0.05f);
                sColor.a-=0.01f;
                startTxt.color = sColor;
            }
            for(int j=0; j<50; j++)
            {
                yield return new WaitForSeconds(0.05f);
                sColor.a+=0.01f;
                startTxt.color = sColor;
            }
        }
    }
}

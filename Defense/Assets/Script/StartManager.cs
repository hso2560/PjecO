using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private bool es = false;
    [SerializeField] GameObject quPanel;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!es)
                es = true;
            else
                es = false;

            quPanel.SetActive(es);
        }
    }
    public void OnClickBack()
    {
        es = false;
        quPanel.SetActive(false);
    }
    public void Onqu()
    {
        Application.Quit();
    }
    public void GoLobby()
    {
        Invoke("goLobby", 0.5f);
    }

    void goLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}

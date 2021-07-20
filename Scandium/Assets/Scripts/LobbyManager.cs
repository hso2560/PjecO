using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject QuitPanel;
    [SerializeField] GameObject HiddenObj;
    [SerializeField] GameObject ReadyPanel, FirPanel;
    [SerializeField] GameObject cam;
    [SerializeField] Text clearTxt;
    [SerializeField] Button DevBtn;
    public bool isCam = true;
    int s=1;
    bool isPanel = false;
    string c;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("SCENENUM"))
            s = PlayerPrefs.GetInt("SCENENUM");
        if (PlayerPrefs.HasKey("CLEAR"))
        {
            clearTxt.gameObject.SetActive(true);
            c = PlayerPrefs.GetString("CLEAR");
            clearTxt.text = c;
            DevBtn.interactable = true;
        }
        Debug.Log(s);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPanel)
            SceneManager.LoadScene("Start");
            else
            {
                isCam = true;
                QuitPanel.SetActive(false);
                isPanel = false;
            }
        }
    }
    public void ClickH()
    {
        isCam = false;
        cam.transform.position = new Vector3(0, 0, -10);
        HiddenObj.SetActive(true);
    }
    public void ClickCH()
    {
        isCam = true;
        HiddenObj.SetActive(false);
    }
    public void OnClickQuit()
    {
        isCam = false;
        cam.transform.position = new Vector3(0, 0, -10);
        HiddenObj.SetActive(false);
        QuitPanel.SetActive(true);
        isPanel = true;
    }

    public void OnBack()
    {
        isPanel = false;
        QuitPanel.SetActive(false);
        isCam = true;
    }
    public void Yes()
    {
        Application.Quit();
    }
    public void ReadyButt()
    {
        isCam = false;
        cam.transform.position = new Vector3(0, 0, -10);
        ReadyPanel.SetActive(true);
    }

    public void ClickMain()
    {
        Invoke("GoMain", 0.5f);
    }

    private void GoMain()
    {
        SceneManager.LoadScene("Main"+ s.ToString()); 
    }
    public void GoMainRe()
    {
        //PlayerPrefs.DeleteAll();   //모든 키값을 삭제하므로 다른 챕터의 진행이나 클리어 표시도 함께 사라질 수가 있어서 DeleteKey사용
        //키값 삭제
        PlayerPrefs.DeleteKey("SCENENUM");
        PlayerPrefs.DeleteKey("SAVE");
        PlayerPrefs.DeleteKey("QUEST");
        PlayerPrefs.DeleteKey("PROGRESS");
        PlayerPrefs.DeleteKey("HELP");
        PlayerPrefs.DeleteKey("HP");
        PlayerPrefs.DeleteKey("MAXHP");
        PlayerPrefs.DeleteKey("ARROW");

        //키값 초기화
        GameManager.saveCount = 0;
        GameManager.sceneNum = 1;
        GameManager.qCount = 0;
        GameManager.proCount = 0;
        GameManager.helpNum = 0;
        SceneManager.LoadScene("Main1");
    }
    public void FromFirPan()
    {
        FirPanel.SetActive(true);
    }
    public void FirX()
    {
        FirPanel.SetActive(false);
    }
    public void OffReadyPan()
    {
        isCam = true;
        ReadyPanel.SetActive(false);
    }
    public void DevelClearDel()
    {
        PlayerPrefs.DeleteKey("CLEAR");
    }
}

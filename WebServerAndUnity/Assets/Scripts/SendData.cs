using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendData : MonoBehaviour
{
    public Button sendDataBtn;
    public Button loginBtn;
    public InputField inputText;

    public int score = 200;
    public string token;

    private void Start()
    {
        sendDataBtn.onClick.AddListener(() =>
        {
            StartCoroutine(Send());
        });
        loginBtn.onClick.AddListener(() =>
        {
            StartCoroutine(Login());
        });
    }

    
    private IEnumerator Send()
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("data", inputText.text);
        form.AddField("token", token);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:54000/save_data", form);
        
        yield return www.SendWebRequest();

        if(www.result!=UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string result = www.downloadHandler.text;

            Debug.Log(result);
        }
    }

    private IEnumerator Login()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:54000/login", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //string result = www.downloadHandler.text;

            //Debug.Log(result);

            ResponseVO vo = JsonUtility.FromJson<ResponseVO>(www.downloadHandler.text);
            token = vo.msg;
        }
    }

}

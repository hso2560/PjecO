using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectManager : MonoBehaviour
{
    public InputField txtIp;
    public InputField txtPort;

    public Button btnConnect;
    public CanvasGroup connectPanel;

    bool isConnected = false;

    private void Start()
    {
        txtIp.text = SocketClient.instance.url;
        txtPort.text = SocketClient.instance.port.ToString();

        btnConnect.onClick.AddListener(() =>
        {
            if (isConnected) return;
            if(txtPort.text.Trim()=="" || txtIp.text.Trim()=="")
            {
                PopupManager.OpenPopup(IconCategory.ERR, "필수값이 비어있습니다.");
                return;
            }

            SocketClient.instance.ConnectSocket(txtIp.text, txtPort.text);

            connectPanel.alpha = 0;
            connectPanel.interactable = false;
            connectPanel.blocksRaycasts = false;

            UIManager.OpenLoginPanel();
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    public string url = "ws://localhost";
    public int port = 34000;

    //public string url2 = "ws://localhost";
    //public int port2 = 34000;

    public GameObject handlers;

    private WebSocket webSocket;

    public static SocketClient instance;
    public static void SendDataToSocket(string json)
    {
        instance.SendData(json);
    }

    private Dictionary<string, IMsgHandler> handlerDictionary;

    private void Awake()
    {
        handlerDictionary = new Dictionary<string, IMsgHandler>();
        DontDestroyOnLoad(gameObject);
        if(instance!=null)
        {
            Debug.Log("소켓 클라이언트가 여러개 실행중");
        }
        instance = this;
    }

    private void Start()
    {
        handlerDictionary.Add("CHAT", handlers.GetComponent<ChatHandler>());
        handlerDictionary.Add("LOGIN", handlers.GetComponent<LoginHandler>());
        handlerDictionary.Add("REFRESH", handlers.GetComponent<RefreshHandler>());
        handlerDictionary.Add("DISCONNECT", handlers.GetComponent<DisconHandler>());
        handlerDictionary.Add("INITDATA", handlers.GetComponent<InitHandler>());
        handlerDictionary.Add("FIRE", handlers.GetComponent<FireHandler>());
        handlerDictionary.Add("HIT", handlers.GetComponent<HitHandler>());
        handlerDictionary.Add("DEAD", handlers.GetComponent<DeadHandler>());
        handlerDictionary.Add("RESPAWN", handlers.GetComponent<RespawnHandler>());

        //webSocket.Send("CHAT:Hello WebServer");
    }
    public void ConnectSocket(string url, string port)
    {
        webSocket = new WebSocket($"{url}:{port}");
        webSocket.Connect();
        webSocket.OnMessage += (sender, e) =>
        {
            ReceiveData((WebSocket)sender, e);
        };
    }
    private void ReceiveData(WebSocket sender, MessageEventArgs e)
    {
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);

        /*if (handlerDictionary.ContainsKey(vo.type))
        {
            IMsgHandler handler = handlerDictionary[vo.type];
            handler.HandleMsg(vo.payload);
        }
        else
        {
            Debug.Log("존재하지 않는 프로토콜 요청 " + vo.type);
        }*/

        IMsgHandler handler = null;
        if(handlerDictionary.TryGetValue(vo.type, out handler))
        {
            handler.HandleMsg(vo.payload);
        }
        else
        {
            Debug.Log("존재하지 않는 프로토콜 요청 " + vo.type);
        }
        
    }

    public void SendData(string json)
    {
        webSocket.Send(json);
    }

    private void OnDestroy()
    {
        if(webSocket.ReadyState==WebSocketState.Connecting)
           webSocket.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        GameManager.DisconnectUser(int.Parse(payload));
    }
}

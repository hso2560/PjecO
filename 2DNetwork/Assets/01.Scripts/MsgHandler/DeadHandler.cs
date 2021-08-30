using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        DeadVO dVO = JsonUtility.FromJson<DeadVO>(payload);

        GameManager.RecordDeadInfo(dVO);
        //GameManager.instance.PlayerDeath(dVO.socketId);

        Debug.Log(dVO.socketId);
    }
}

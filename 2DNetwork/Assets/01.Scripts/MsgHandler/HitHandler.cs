using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        HitInfoVO hitVO = JsonUtility.FromJson<HitInfoVO>(payload);
        //PlayerRPC rpc = GameManager.instance.GetPlayerRPC(hitVO.socketId);
        GameManager.RecordHitInfo(hitVO);
    }
}

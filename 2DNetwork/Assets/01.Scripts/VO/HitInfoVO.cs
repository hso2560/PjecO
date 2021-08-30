using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitInfoVO 
{
    public int socketId;
    public int hp;

    public HitInfoVO(int socketId, int hp)
    {
        this.socketId = socketId;
        this.hp = hp;
    }
}

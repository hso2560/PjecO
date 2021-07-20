using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Developer : MonoBehaviour
{
    private PlayerMove player;
    [SerializeField] int testNum;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    public void ClickTest()
    {
        if (testNum == 0)
        {
            player.HpManage = 30;
        }
        else if (testNum == 1)
            player.Damage(30);
        else if (testNum == 2)
            player.TCol();
        else if (testNum == 3)
            player.TColN();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMng : MonoBehaviour
{
    PlayerMove player;
    public int num;   //어느 곳에서 어느곳으로 플레이어 위치를 넘길지 정하기 위해서 각 오브젝트마다 숫자를 정함

    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (num == 0)
                player.transform.position = new Vector2(-53, -47);
            else
                player.transform.position = new Vector2(-53, -17);
        }
    }
}

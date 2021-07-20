using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardNum : MonoBehaviour
{
    public int num;

    private void Awake()
    {
        num = Random.Range(0, 10);

        if (num == 10)
            num = 5;  
    }
}

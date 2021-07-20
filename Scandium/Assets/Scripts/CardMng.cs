using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMng : MonoBehaviour
{
    [SerializeField] private CardNum[] card = new CardNum[4];
    [SerializeField] private Vector2[] ranPos;

    public int r, o, y, g;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            Re:
            int k = Random.Range(0, ranPos.Length);
            for (int j = 0; j < i; j++)
            {
                if((Vector2)card[j].transform.position==ranPos[k])
                {
                    goto Re;
                }
            }
            card[i].transform.position = ranPos[k];    
        }

        Invoke("NumAl", 0.2f);
    }

    private void NumAl()
    {
        r = card[0].num;
        o = card[1].num;
        y = card[2].num;
        g = card[3].num;
    }
}

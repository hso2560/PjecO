using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrTest : MonoBehaviour
{
    public GameObject pref;

    private void Start()
    {
        for(int i=0; i<=360; i += 10)
        {
            /*Vector2 v = new Vector2(i/30, Mathf.Sin(i*Mathf.Deg2Rad));        //Cos, Tan ¸ð¾çµµ ³ª¿È
            Instantiate(pref, new Vector2(v.x-4,v.y+7),Quaternion.identity);*/
        }
        for(int i=-15; i<=15; i++)
        {
            /*for(int j=0; j<=15; j++)
            {
                if (i * i + j * j <= 15 * 15)
                {
                    Instantiate(pref, new Vector2(i+3, j-4), Quaternion.identity);
                }
            }*/
        }
    }
}

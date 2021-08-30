using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrTest2 : MonoBehaviour
{
    public float runningTime=0;
    public float speed=5;
    public float radius = 4;

    private void Update()
    {
        /* float x = transform.position.x + 0.1f;
         float y = Mathf.Sin(x * Mathf.Deg2Rad);
         transform.Translate(Vector2.right*Mathf.Sin(Time.time) * 5 * Time.deltaTime);*/

        /*runningTime += Time.deltaTime * speed;
        float x = radius * Mathf.Cos(runningTime);
        float y = radius * Mathf.Sin(runningTime);
        transform.position = new Vector2(-x, -y);

        radius -= Time.deltaTime; */

        //transform.RotateAround(new Vector3(0, 0, 0),transform.forward ,3f);


    }
}

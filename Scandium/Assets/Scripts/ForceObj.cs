using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObj : MonoBehaviour
{
    Rigidbody2D rigid;
    public float force = 20;
    public bool disc;
    public Vector2 LimitMax, LimitMin;

    float ran;
    public void ForceStart()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (disc)
        {
            ran = Random.Range(-0.5f, 0.5f);
            rigid.AddForce(new Vector2(ran, 0.8f) * force * Time.deltaTime);
            InvokeRepeating("GraUp", 0.5f, 0.3f);
            Invoke("ForceStop", 2.3f);
        }
        else
        {
            ran = Random.Range(-0.5f, 0.5f);
            rigid.AddForce(new Vector2(ran, 0.8f) * force * Time.deltaTime);
            InvokeRepeating("GraUp", 0.5f, 0.3f);
            Invoke("ForceStop", 2f);
        }
    }

    void GraUp()
    {
        rigid.gravityScale += 0.2f;
    }

    private void ForceStop()
    {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        CancelInvoke("GraUp");
    }

    private void Update()
    {
        float x = Mathf.Clamp(transform.position.x, LimitMin.x, LimitMax.x);
        float y = Mathf.Clamp(transform.position.y, LimitMin.y, LimitMax.y);
        transform.position = new Vector2(x, y);
    }
}

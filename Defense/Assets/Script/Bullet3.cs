using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    public int damage;
    private void Update()
    {
        transform.Rotate(Vector3.forward * 10*speed*Time.deltaTime);
    }
}

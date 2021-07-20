﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] float offsetX = 0f;
    [SerializeField] float offsetY = 0f;
    [SerializeField] float offsetZ = 0f;
    [SerializeField] Vector3 limitMin;
    [SerializeField] Vector3 limitMax;
    private Vector3 cameraPosition;
    public bool isMov = true;

    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;
    Quaternion m_originRot;

    private void Start()
    {
        m_originRot = transform.rotation;
    }
    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.A))  //테스트용
        {
            StartCoroutine(ShakeCorou());
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            StartCoroutine(Reset());
        }*/
    }


    private void LateUpdate()
    {
        if (isMov)
        {
            cameraPosition.x = player.transform.position.x + offsetX;
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, limitMin.x, limitMax.x);

            cameraPosition.y = player.transform.position.y + offsetY;
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, limitMin.y, limitMax.y);

            cameraPosition.z = player.transform.position.z + offsetZ;

            transform.position = cameraPosition;
        }
    }

    private IEnumerator ShakeCorou()
    {
        Vector3 t_originEuler = transform.eulerAngles;
        while(true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);

            Vector3 t_randomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            while(Quaternion.Angle(transform.rotation,t_rot)>0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator Reset()
    {
        while(Quaternion.Angle(transform.rotation,m_originRot)>0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCorou());
    }
    public void StopShake()
    {
        StopAllCoroutines();
        StartCoroutine(Reset());
    }
}

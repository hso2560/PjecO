using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCam : MonoBehaviour
{
    LobbyManager manager;
    private float dist;
    private Vector3 MouseStart;
    private Vector3 derp;
    float X;
    float Y;

    void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
        dist = transform.position.z;  // Distance camera is above map
    }

    void Update()
    {
        if (manager.isCam)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
                MouseStart.z = transform.position.z;

            }
            else if (Input.GetMouseButton(0))
            {
                var MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
                MouseMove.z = transform.position.z;
                transform.position = transform.position - (MouseMove - MouseStart);
            }
            X = Mathf.Clamp(transform.position.x, 0f, 22f);
            Y = Mathf.Clamp(transform.position.x, 0f, 0f);
            transform.position = new Vector3(X, Y, dist);
        }
    }
}

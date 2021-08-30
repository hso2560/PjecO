using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    //private Stack<TrInfo> trInfos = new Stack<TrInfo>();
    private List<TrInfo> trInfos = new List<TrInfo>();
    private bool isRewind = false;

    void Update()
    {
        if(!isRewind)
        {
            Record();
        }
        else
        {
            TimeRewind();
        }

        KeyInput();
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRewind = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRewind = false;
    }

    private void Record()
    {
        trInfos.Add(new TrInfo(transform.position, transform.rotation));
        //trInfos.Push(new TrInfo(transform.position, transform.rotation));
    }
    private void TimeRewind()
    {
        //TrInfo ti = trInfos.Pop();
        //transform.position = ti.vec;
        //transform.rotation = ti.rot;

        if (trInfos.Count == 0)
        {
            Record();
            return;
        }

        TrInfo ti = trInfos[trInfos.Count - 1];
        transform.position = ti.vec;
        transform.rotation = ti.rot;
        trInfos.RemoveAt(trInfos.Count - 1);
    }
}

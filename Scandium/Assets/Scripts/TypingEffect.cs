using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    private string targetMsg;
    [SerializeField] int CharPerSeconds;
    Text msgText;
    private int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        interval = 1.0f / CharPerSeconds;
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }
    void EffectStart()
    {
        msgText.text = "";
        index = 0;

        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if(msgText.text==targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        Debug.Log("Finish");
    }
}

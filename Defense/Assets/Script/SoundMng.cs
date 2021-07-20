using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour
{
    AudioSource audio;
    bool isSound = true;
    public static bool isEffectS = true;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();  
    }

    public void ClickSound()
    {
        if (isSound)
        {
            isSound = false;
            audio.volume = 0;
        }
        else
        {
            isSound = true;
            audio.volume = 0.1f;
        }
    }
    public void ClickEffectSound()
    {
        if (isEffectS)
        {
            isEffectS = false;
        }
        else
            isEffectS = true;
    }
}

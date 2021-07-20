using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMMng : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    public Slider slider;

    private void Update()
    {
        audio.volume = slider.value;
    }

    public void PitUp(bool b)
    {
        if (b)
            audio.pitch = 3;
        else
            audio.pitch = 1;
    }
}

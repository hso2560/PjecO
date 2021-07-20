using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] private AudioClip[] clip;
    [SerializeField] private Slider soundSlider;
     
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Sound0()
    {
        audio.clip = clip[0];
        audio.loop = true;
        audio.Play();
        
    }
    public void Sound1()
    {
        audio.clip = clip[1];
        audio.loop = false;
        audio.Play();
        
    }
    public void Sound2()
    {
        audio.clip = clip[2];
        audio.loop = true;
        audio.Play();
        
    }
    public void WoodSound()
    {
        audio.clip = clip[3];
        audio.loop = false;
        audio.Play();
    }
    public void SoundStop()
    {
        audio.Stop();
    }

    private void Update()
    {
        audio.volume = soundSlider.value;   
    }
    
    private IEnumerator VolumeDown()
    {
        while(audio.volume>0)
        {
            soundSlider.value -= 0.01f;
            yield return new WaitForSeconds(0.045f);
        }
    }
    public void VVVV()
    {
        StartCoroutine(VolumeDown());
    }

}

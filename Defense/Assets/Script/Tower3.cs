using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower3 : MonoBehaviour
{
    [SerializeField] private GameObject wind;
    [SerializeField] private float delay = 3.5f;  //공격x
    [SerializeField] private float delay2 = 2.5f;  //공격 on
    UpgradeMng upgradeMng;
    public int Lv=1;
    public int inc = 0;
    public int Damage = 30;
    public int gold = 0;
    public Tile tile;
    AudioSource audio;
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        StartCoroutine(WideAttack());
        upgradeMng = FindObjectOfType<UpgradeMng>();
        audio = GetComponent<AudioSource>();
    }
   protected void Update()
    {
        if (SoundMng.isEffectS)
            audio.volume = 0.5f;
        else
            audio.volume = 0;
    }
    IEnumerator WideAttack()
    {
        while(true)
        {
            Bullet3 bullet = wind.GetComponent<Bullet3>();
            bullet.damage = Damage+inc;
            yield return new WaitForSeconds(delay);
           
            audio.Play();
            wind.SetActive(true);
            yield return new WaitForSeconds(delay2);
            audio.Stop();
            wind.SetActive(false);
        }
    }
    public void TextSend()
    {
        upgradeMng.Lv.text = string.Format("레벨: {0}", Lv);
        upgradeMng.speed.text = string.Format("공격속도: {0}", delay);
        upgradeMng.Feature.text = "<color=#2AC9FF>특성: "+ delay2.ToString() +"초간 회오리 공격</color>";
    }
}

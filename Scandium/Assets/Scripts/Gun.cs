using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public short hp = 2;
    Collider2D col;
    public float cool;  //총 재발사 대기시간
    public bool atk=false;  //현재 총을 공격할 수 있는지 확인
    SpriteRenderer sp;
    Color i;
    public Text tx;

    Coroutine cor;
    [SerializeField] AudioClip[] clip;
    [SerializeField] AudioSource audio;
    [SerializeField] BGMMng B;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        i = sp.color;
    }

    public void StartGun()
    {
       
        cool = Random.Range(40f, 45f);
        cor=StartCoroutine(Shot());
        Invoke("DelayAtk", cool - 25);
    }
    void DelayAtk(){
        tx.gameObject.SetActive(true);
        tx.text = "곧 추격탄이 발사됩니다. 총을 부숴주세요.";
        atk = true;
        Invoke("Offtx", 5);
        audio.clip = clip[1];
        audio.Play();
    }
    public void StartHpEffe()
    {
        StartCoroutine(HpEffect());
    }
    void Offtx()
    {
        tx.gameObject.SetActive(false);
    }

    IEnumerator HpEffect()
    {

        sp.color = new Color(1, 1, 1, 0.1f);
        yield return new WaitForSeconds(0.2f);
        sp.color = i;
        yield return new WaitForSeconds(0.2f);
        sp.color = new Color(1, 1, 1, 0.1f);
        yield return new WaitForSeconds(0.2f);
        sp.color = i;
    }

    IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(cool);
            GameObject bullet = PoolManager.instance.GetQueue2();
            bullet.transform.position = transform.position;
            audio.clip = clip[0];
            audio.Play();
            cool = Random.Range(40f, 45f);
            atk = false;
            Invoke("DelayAtk", cool - 25);
        }
    }

    public void StopShot()
    {
        StopCoroutine(cor);
        sp.color = i;
        CancelInvoke("Offtx");
        tx.gameObject.SetActive(true);
        tx.text = "추격탄 발사를 막았습니다.";
        Invoke("Offtx",4);
        hp = 2;
        cool = Random.Range(40f, 45f);
        atk = false;
        cor = StartCoroutine(Shot());
        Invoke("DelayAtk", cool - 25);
    }

    private void Update()
    {
        audio.volume = B.slider.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour
{
    [SerializeField] private Image delayImage;
    public float t=0;   //화살을 쏘고 재공격 대기 중에 몇 초가 흘렀는지 봄
    [SerializeField] Text inform;
    TalkManager tManager;

    private void Awake()
    {
        tManager = FindObjectOfType<TalkManager>();
    }

    private void Update()
    {
        delayImage.fillAmount = t / 0.5f; //0.5f는 화살의 재공격 대기시간.
    }

    public void FuncTFlow()
    {
        StartCoroutine(TFlow());
    }

    private IEnumerator TFlow()
    {
        while (t > 0)
        {
            yield return new WaitForSeconds(0.01f);
            t -= 0.01f;
        }
    }

    public void Inform()
    {
        inform.text = "화살 " + tManager.pickup.ToString() + "개를 획득하였습니다";
        OnTxt();
    }
    public void OnTxt()
    {
        inform.color = Color.green;
        Invoke("gradually", 2f);
    }
    void gradually()
    {
        StartCoroutine(OffTxt());
    }
    IEnumerator OffTxt()
    {
        float i = 0.99f;
        while (i > 0)
        {
            inform.color = new Color(0, 1, 0, i);
            i -= 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
    }
}

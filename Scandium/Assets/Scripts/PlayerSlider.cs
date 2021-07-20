using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    private PlayerMove player;
    private Slider slider;
    private bool isEffect = false;
    [SerializeField] Text HpTxt;
    [SerializeField] GameObject fill;
    [SerializeField] float Delay = 0.5f;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        player = FindObjectOfType<PlayerMove>();
        
     
    }
    private void Update()
    {
        slider.value = (float)player.HpManage / (float)player.MaxHP();
        HpTxt.text = string.Format("HP: {0}/{1}", player.HpManage, player.MaxHP());
        if(player.HpManage<30)
        {
            if(!isEffect)
            {
                isEffect = true;
                InvokeRepeating("HpEffect", 0, Delay*2);
            }
        }
        else
        {
            isEffect = false;
            CancelInvoke("HpEffect");
            CancelInvoke("HpEffect2");
            fill.SetActive(true);
        }
    }
    void HpEffect()
    {
        fill.SetActive(false);
        Invoke("HpEffect2", Delay);
    }
    void HpEffect2()
    {
        fill.SetActive(true);
    }
}

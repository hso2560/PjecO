using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlider : MonoBehaviour
{
    [SerializeField] RealEnemy enemy;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = (float)enemy.GetHp() / (float)enemy.GetStartHp();
        transform.rotation =Quaternion.Euler(0,0,0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] EnemyMove enemy;
    [SerializeField] EnemyMove2 enemy2;
    [SerializeField] EnemyMove3 enemy3;
    [SerializeField] EnemyMove4 enemy4;
    [SerializeField] EnemyMove5 enemy5;
    [SerializeField] EnemyMove6 enemy6;
    GameManager manager;

    private void Awake()
    {

        slider = GetComponent<Slider>();
        manager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (manager.wave <= 5)
            slider.value = (float)enemy.ShowHp() / (float)enemy.ShowFirstHp();
        else if (manager.wave <= 10)
            slider.value = (float)enemy2.ShowHp() / (float)enemy2.ShowFirstHp();
        else if (manager.wave <= 15)
            slider.value = (float)enemy3.ShowHp() / (float)enemy3.ShowFirstHp();
        else if (manager.wave <= 20)
            slider.value = (float)enemy4.ShowHp() / (float)enemy4.ShowFirstHp();
        else if(manager.wave<=25)
            slider.value = (float)enemy5.ShowHp() / (float)enemy5.ShowFirstHp();
        else if (manager.wave <= 30)
            slider.value = (float)enemy6.ShowHp() / (float)enemy6.ShowFirstHp();
    }
}

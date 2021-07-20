using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlider : MonoBehaviour
{
    Slider slider;
    EnemyMove enemyMove;

   
    private void Awake()
    {

        slider = GetComponent<Slider>();
        
    }
    public void Setup(EnemyMove enemyMove)
    {
        this.enemyMove = enemyMove;
    }

    private void Update()
    {
        slider.value = (float)enemyMove.ShowHp() / (float)enemyMove.ShowFirstHp();
       
       
       
    }

   
}

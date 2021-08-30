using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject infoPrefab;
    public CanvasGroup cvsLogin;

    private void Awake()
    {
        instance = this;
        PoolManager.CreatePool<InfoUI>(infoPrefab, transform, 8); //8���� HP�ٿ� Text ����
        

       
    }

    public static void OpenLoginPanel()
    {
        instance.cvsLogin.alpha = 1;
        instance.cvsLogin.interactable = true;
        instance.cvsLogin.blocksRaycasts = true;
    }

    public static InfoUI SetInfoUI(Transform player, string name)
    {
        InfoUI ui = PoolManager.GetItem<InfoUI>();
        ui.SetTarget(player, name);
        return ui;
    }

    public static void CloseLoginPanel()
    {

        Sequence seq = DOTween.Sequence();
        seq.Append(
            DOTween.To(
                () => instance.cvsLogin.alpha,
                value => instance.cvsLogin.alpha = value,
                0f,
                1f));
        seq.AppendCallback(() => {
            instance.cvsLogin.interactable = false;
            instance.cvsLogin.blocksRaycasts = false;
        });
    }
}
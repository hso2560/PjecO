using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum IconCategory
{
    ERR = 0,
    SYSTEM =1
}

public class PopupManager : MonoBehaviour
{
    private static PopupManager instance = null;
    public Sprite[] icons;

    [Header("팝업셋팅관련")]
    public GameObject popupPanel;
    public Image popupIcon;
    public Text popupText;
    public Transform popupTrm;
    public Button closeBtn;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 PopupManager가 실행되고 있습니다.");
        }
        instance = this;
        closeBtn.onClick.AddListener(() =>
        {
            instance.popupPanel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            instance.popupPanel.SetActive(false);
            //instance.popupPanel.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
        });
    }

    private void Start()
    {
        popupPanel.SetActive(false);
        popupPanel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public static void OpenPopup(IconCategory icon, string text)
    {
        instance.popupPanel.SetActive(true);
        instance.popupPanel.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        instance.popupIcon.sprite = instance.icons[(int)icon];
        instance.popupText.text = text;
    }
}

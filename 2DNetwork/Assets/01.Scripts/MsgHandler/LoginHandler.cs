using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour, IMsgHandler
{
    private TankCategory tank;
    public Image[] selectImageHolders;
    public Sprite[] selectImages;
    public InputField nameInput;

    public Button redTankBtn;
    public Button blueTankBtn;

    public Button loginBtn;

    private void Awake()
    {
        tank = TankCategory.Red;
        redTankBtn.onClick.AddListener(() =>
        {
            tank = TankCategory.Red;
           foreach(Image img in selectImageHolders)
            {
                img.sprite = selectImages[1];
            }
            selectImageHolders[(int)TankCategory.Red].sprite = selectImages[0];
        });
        blueTankBtn.onClick.AddListener(() =>
        {
            tank = TankCategory.Blue;
            foreach (Image img in selectImageHolders)
            {
                img.sprite = selectImages[1];
            }
            selectImageHolders[(int)TankCategory.Blue].sprite = selectImages[0];
        });
        loginBtn.onClick.AddListener(() =>
        {
            string name = nameInput.text;

            if (name.Trim()=="")
            {
                PopupManager.OpenPopup(IconCategory.SYSTEM, "이름은 반드시 입력하셔야 합니다.");
                return;
            }

            LoginVO loginVO = new LoginVO(tank,name);

            string payload = JsonUtility.ToJson(loginVO);
            DataVO dataVO = new DataVO();
            dataVO.type = "LOGIN";
            dataVO.payload = payload;

            string json = JsonUtility.ToJson(dataVO);
            SocketClient.SendDataToSocket(json);
        });
    }
    public void HandleMsg(string payload)
    {
        Debug.Log(payload);
        TransformVO vo = JsonUtility.FromJson<TransformVO>(payload);
        GameManager.GameStart(vo);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerRPC rpc;
    public int maxHP;
    public int currentHP;

    public bool isEnemy=false;
    public InfoUI ui;

    private void Awake()
    {
        rpc = GetComponent<PlayerRPC>();
    }

    public void SetHealthScript(TankDataVO data, bool isEnemy, InfoUI ui)
    {
        maxHP = data.maxHP;
        currentHP = maxHP;
        this.isEnemy = isEnemy;
        this.ui = ui;
       
    }

    public void OnDamage(int damage, Vector2 powerDir, bool isEnemy, int shooterId)
    {
        if (rpc.isRemote) return;

        currentHP -= damage;
        UpdataUI();
        HitInfoVO vo = new HitInfoVO(GameManager.instance.socketId, currentHP);
        string payload = JsonUtility.ToJson(vo);
        DataVO dataVo = new DataVO();
        dataVo.type = "HIT";
        dataVo.payload = payload;

        SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVo));

        if (currentHP <= 0)
        {
            Die(shooterId);
        }
    }
    public void UpdataUI()
    {
        ui.UpdataHPBar((float)currentHP / (float)maxHP);
        //float fhp = (float)currentHP / (float)maxHP;
        //ui.FillImg.transform.localScale = new Vector2(fhp, 1);
        //Debug.Log(fhp);
    }
   
    public void Die(int shooterId=0)
    {
        if (rpc.isDead) return;

        MassiveExplo mExp= EffectManager.GetMassiveExplo();
        mExp.ResetPos(transform.position);

        //gameObject.SetActive(false);
        //ui.gameObject.SetActive(false);
        if (!isEnemy)
        {
           
            DeadVO vo = new DeadVO(GameManager.instance.socketId, shooterId);
            string payload = JsonUtility.ToJson(vo);

            DataVO dataVo = new DataVO();
            dataVo.type = "DEAD";
            dataVo.payload = payload;
            SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVo));
            
            GameManager.instance.SetPlayerDead();
            
            rpc.SetScript(false);
            rpc.isDead = true;
            //ui.SetVisible(false);
        }
    }
}

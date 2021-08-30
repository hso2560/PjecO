using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPC : MonoBehaviour
{
    public Sprite[] bodys;
    public Sprite[] turrets;

    public SpriteRenderer bodyRenderer;
    public SpriteRenderer turretRenderer;
    public bool isRemote = false;
    private PlayerHealth health;

    private PlayerInput input;
    private PlayerMove move;

    private TankCategory tankCategory;
    private WaitForSeconds ws = new WaitForSeconds(1 / 10);

    private Vector3 targetPosition, targetRotation, targetTurretRotation;
    public float lerpSpeed = 4f;
    private InfoUI ui = null;

    private PlayerFire fire;

    public bool isDead = false;

    private BoxCollider2D collider;
    private SpriteRenderer[] renderers;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMove>();
        fire = GetComponent<PlayerFire>();
        health = GetComponent<PlayerHealth>();

        collider = GetComponent<BoxCollider2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void InitPlayer(Vector3 pos, TankCategory tank,InfoUI ui ,bool remote = false)
    {
        bodyRenderer.sprite = bodys[(int)tank];
        turretRenderer.sprite = turrets[(int)tank];
        tankCategory = tank;
        transform.position = pos;
        this.ui = ui;

        isRemote = remote;

        if (isRemote)
        {
            input.enabled = false;
            move.enabled = false;
            gameObject.layer = GameManager.EnemyLayer;
        }
        else
        {
            input.enabled = true;
            move.enabled = true;
            gameObject.layer = GameManager.PlayerLayer;
            move.SetMoveScript(GameManager.tankDataDic[tank]);
            
            StartCoroutine(SendData());
            
        }

        health.SetHealthScript(GameManager.tankDataDic[tank], remote, ui);
        fire.SetFireScript(GameManager.tankDataDic[tank], remote);
    }



    IEnumerator SendData()
    {
        int socketId = GameManager.instance.socketId;

        while (true)
        {
            yield return ws;
            TransformVO vo = new TransformVO(transform.position, transform.rotation.eulerAngles
                , turretRenderer.transform.rotation.eulerAngles,
                socketId, tankCategory);
            string payload = JsonUtility.ToJson(vo);

            DataVO dataVo = new DataVO();
            dataVo.type = "TRANSFORM";
            dataVo.payload = payload;
            SocketClient.SendDataToSocket(JsonUtility.ToJson(dataVo));
        }
    }

    public void SetTransform(Vector3 pos, Vector3 rot, Vector3 trtRot)
    {
        if (isRemote)
        {
            targetPosition = pos;
            targetRotation = rot;
            targetTurretRotation = trtRot;
        }
    }

    private void Update()
    {
        if (isRemote)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, targetRotation.z, Time.deltaTime * lerpSpeed));

            turretRenderer.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(turretRenderer.transform.rotation.eulerAngles.z, targetTurretRotation.z, Time.deltaTime * lerpSpeed));
        }
    }

    public void SetDisable()
    {
        ui.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SetHP(int hp)
    {
        health.currentHP = hp;
        health.UpdataUI();
    }

    public void SetDead()
    {
        
        isDead = true;
        health.Die();
        SetScript(false);
    }

    public void SetScript(bool on)
    {
        input.enabled = on && !isRemote;
        collider.enabled = on;
        foreach(SpriteRenderer s in renderers)
        {
            s.enabled = on;
        }
        ui.SetVisible(on);
        //ui.gameObject.GetComponent
    }

    public void Respawn()
    {
        isDead = false;
        SetScript(true);
        ui.SetVisible(true);
        health.currentHP = health.maxHP;
        health.UpdataUI();

        DataVO vo = new DataVO();
        vo.type = "RESPAWN";
        vo.payload = GameManager.instance.socketId + "";

        SocketClient.SendDataToSocket(JsonUtility.ToJson(vo));
    }

    public void UIInvTest(bool on)=> ui.SetVisible(on);
}
using System;
using UnityEngine;

[Serializable]
public class FireInfoVO
{
    public int socketId;
    public Vector3 position;
    public Vector3 direction;
    public float speed;
    public int damage;
    public TransformVO transform;

    public FireInfoVO(int socketId, Vector3 position, Vector3 dir, float speed, int dmg, TransformVO transform)
    {
        this.socketId = socketId;
        this.position = position;
        this.direction = dir;
        this.speed = speed;
        this.damage = dmg;
        this.transform = transform;
    }
}

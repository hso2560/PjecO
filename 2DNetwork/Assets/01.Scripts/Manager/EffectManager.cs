using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject explosionPrefab, massivePrefab;

    private void Awake()
    {
        PoolManager.CreatePool<Explosion>(explosionPrefab, transform, 10);
        PoolManager.CreatePool<MassiveExplo>(massivePrefab, transform, 5);
    }

    public static Explosion GetExplosion()
    {
        return PoolManager.GetItem<Explosion>();
    }

    public static MassiveExplo GetMassiveExplo()
    {
        return PoolManager.GetItem<MassiveExplo>();
    }
}

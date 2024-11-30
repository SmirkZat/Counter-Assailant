using System;
using UnityEngine;

public class Heart : MonoBehaviour, ICollectible
{
    public static Action<Heart> OnHeartCollected;
    public int HealthRestoration => healthRestoration;

    [SerializeField] int healthRestoration = 1;

    public void Collect()
    {
        Destroy(gameObject);
        OnHeartCollected?.Invoke(this);
    }

    public void MagnetToTarget(Transform target)
    {
    }
}

using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public static Action<Coin> OnCoinCollected;
    public int CoinAmount => coinAmount;

    [SerializeField] int coinAmount = 10;
    [SerializeField] ParticleSystem coinFVX;

    bool isMagnetized = false;

    Transform target;

    void Update()
    {
        if (isMagnetized)
        {
            GoToTarget();
        }
    }


    public void Collect()
    {
        Destroy(gameObject);
        Instantiate(coinFVX, transform.position, Quaternion.identity);
        OnCoinCollected?.Invoke(this);
    }

    public void MagnetToTarget(Transform target)
    {
        this.target = target;
        isMagnetized = true;
    }

    void GoToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 50 * Time.deltaTime);
    }

}

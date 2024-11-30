using System;
using UnityEngine;

public class Gem : MonoBehaviour, ICollectible
{
    public static Action OnGemCollected;

    [SerializeField] ParticleSystem gemVFX;

    Transform target;
    bool isMagnetized = false;

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
        Instantiate(gemVFX, transform.position, Quaternion.identity);
        OnGemCollected?.Invoke();
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

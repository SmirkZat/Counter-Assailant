using UnityEngine;

public class EnemyCombat : MonoBehaviour, IDamageable
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] float knockBackThrust = 10f;

    KnockBack knockBack;

    void Awake()
    {
        knockBack = GetComponent<KnockBack>();
    }

    void HandleEnemyKnockBack()
    {
        if (knockBack != null)
        {
            Vector3 directionToPlayer = (transform.position - PlayerController.Instance.transform.position).normalized;
            knockBack.GetKnockBack(directionToPlayer, knockBackThrust);
        }
    }

    public void TakeDamage(float damage)
    {
        GetComponent<Flash>()?.StartFlash();
        GetComponent<Health>()?.TakeDamage(damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HandleEnemyKnockBack();
            other.GetComponent<IDamageable>()?.TakeDamage(damageAmount);
        }
    }
}

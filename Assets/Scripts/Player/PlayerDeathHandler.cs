using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;

    Health health;
    Animator animator;

    readonly int DEATH_ANIMATION = Animator.StringToHash("DeathAnimation");

    void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }

    void HandleDeath(Health sender)
    {
        Instantiate(deathVFX, transform.position, transform.rotation);
        animator.Play(DEATH_ANIMATION);
        // Die();
    }

    void Die()
    {
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }
}

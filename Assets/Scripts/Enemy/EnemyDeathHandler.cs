using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;

    Health health;
    Animator animator;
    LootBag lootBag;
    LevelManager levelManager;
    Transform lootParentTransform;

    readonly int DEATH_ANIMATION = Animator.StringToHash("DeathAnimation");

    void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        lootBag = GetComponent<LootBag>();
    }

    void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }

    public void Setup(LevelManager levelManager, Transform lootParentTransform)
    {
        this.levelManager = levelManager;
        this.lootParentTransform = lootParentTransform;
    }

    void HandleDeath(Health sender)
    {
        PlayDeathAnimation();
    }

    public void PlayDeathAnimation()
    {
        Instantiate(deathVFX, transform.position, transform.rotation);
        animator.Play(DEATH_ANIMATION);
    }

    // being called has animation event
    void Die()
    {
        // create pickup
        lootBag.InstantiateLoot(transform.position, lootParentTransform);
        Destroy(gameObject);
        levelManager?.DecreaseEnemiesInWave();
    }
}

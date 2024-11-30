using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] FireBall fireBallPrefab;
    [SerializeField] GameObject body;
    [SerializeField] Transform fireballSpawner;
    [SerializeField] float fireballSpeed = 50f;
    [SerializeField] int numberOfFireballs = 10;
    [SerializeField] ParticleSystem explosionFVX;
    [SerializeField] int maxFireBallsPerShoot;

    readonly int DEATH_ANIMATION_HASH = Animator.StringToHash("DeathAnimation");

    Health health;
    Animator animator;
    ObjectPool<FireBall> objectPool;

    bool canAttack = false;

    void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        CreateFireballPool();
    }

    void Start()
    {
        GameManager.Instance.AudioManager.ChangeLevelMusic(LevelsEnum.BossLevel);
        StartCoroutine(ShootProjectiles());
    }

    void OnEnable()
    {
        health.OnDeath += PlayDeathAnimation;
        LevelTransitionHandler.OnFadeOutLevelComplete += LoadCutscene;
    }

    void OnDisable()
    {
        health.OnDeath -= PlayDeathAnimation;
        LevelTransitionHandler.OnFadeOutLevelComplete -= LoadCutscene;
    }

    public void ReleaseFireballFromPool(FireBall fireBall)
    {
        objectPool.Release(fireBall);
    }

    void CreateFireballPool()
    {
        objectPool = new ObjectPool<FireBall>(() =>
            {
                return Instantiate(fireBallPrefab);
            }, fireBall =>
            {
                fireBall?.gameObject?.SetActive(true);
            }, fireBall =>
            {
                fireBall?.gameObject?.SetActive(false);
            }, fireBall =>
            {
                Destroy(fireBall);
            }, false, maxFireBallsPerShoot, numberOfFireballs * 2);
    }

    IEnumerator ShootProjectiles()
    {
        while (true)
        {
            ShootFireballs();
            yield return new WaitForSeconds(3f);
        }
    }

    void ShootFireballs()
    {
        float startAngle = 90f;
        float endAngle = 270f;

        int numOfFireballs = Random.Range(numberOfFireballs, maxFireBallsPerShoot);
        float angleStep = (endAngle - startAngle) / (numOfFireballs - 1);


        for (int i = 0; i < numOfFireballs; i++)
        {
            // Calculate angle for this projectile
            float angle = startAngle + (angleStep * i);
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            if (objectPool != null && !health.isDead && canAttack)
            {
                FireBall pooledFireball = objectPool.Get();
                pooledFireball.Init(this, fireballSpawner.position, direction * fireballSpeed);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    void StartAttack()
    {
        animator.SetBool("CanAttack", true);
        canAttack = true;
    }

    void PlayDeathAnimation(Health health)
    {
        animator.Play(DEATH_ANIMATION_HASH);
    }

    public void Die()
    {
        canAttack = false;
        body.SetActive(false);
        Instantiate(explosionFVX, fireballSpawner.position, transform.rotation);
    }

    void HandleEndLevel()
    {
        LevelTransitionHandler.Instance.PlayLevelFadeOutAnimation();
    }

    void LoadCutscene()
    {
        UIManager.Instance.ToggleCursorCrosshair(true);
        GameManager.Instance.LoadEndCutscene();
    }

}

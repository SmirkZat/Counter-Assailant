using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject hitVFX;

    Gun _gun;
    Vector2 fireDirection;
    Rigidbody2D rb;

    float damageAmount;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        rb.velocity = fireDirection * bulletSpeed;
    }

    public void Init(Gun gun, Vector2 bulletSpawnPos, Vector2 mousePos, float damage)
    {
        _gun = gun;
        transform.position = bulletSpawnPos;
        fireDirection = (mousePos - bulletSpawnPos).normalized;
        damageAmount = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            // Flash flash = other.GetComponent<Flash>();
            // Health health = other.GetComponent<Health>();
            // flash?.StartFlash();
            // health?.TakeDamage(damageAmount);
            other.GetComponent<IDamageable>()?.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}

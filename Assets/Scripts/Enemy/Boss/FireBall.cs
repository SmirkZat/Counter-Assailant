using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Vector2 velocity;

    Rigidbody2D rb;

    Boss bossShooter;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    public void Init(Boss boss, Vector2 spawnPosition, Vector2 velocity)
    {
        this.velocity = velocity;
        transform.position = spawnPosition;
        bossShooter = boss;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Projectile") && !other.CompareTag("Enemy"))
        {
            bossShooter.ReleaseFireballFromPool(this);
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(2);
        }
    }
}

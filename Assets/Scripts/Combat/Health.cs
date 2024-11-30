using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action<Health> OnDeath;
    public Action<Health> OnMaxHealthIncreased;
    public static Action OnHealthAdd;

    public int StartingHealth => startingHealth;
    public int CurrentHealth => currentHealth;
    public bool isDead => currentHealth <= 0 || damageFloatingPoint <= 0;
    public bool isEnemyDead => damageFloatingPoint <= 0;

    [SerializeField] int startingHealth = 3;

    int currentHealth;
    float damageFloatingPoint;


    void Awake()
    {
        ResetHealth();
    }

    public void IncreaseMaxHealth(int healthAmount)
    {
        startingHealth += healthAmount;
        ResetHealth();
        OnMaxHealthIncreased?.Invoke(this);
    }

    void ResetHealth()
    {
        currentHealth = startingHealth;
        damageFloatingPoint = startingHealth;
    }

    public void AddHealth(int amount)
    {
        OnHealthAdd?.Invoke();
        if ((currentHealth + amount) >= startingHealth)
        {
            ResetHealth();
            return;
        }
        currentHealth += amount;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke(this);
        }
    }

    public void TakeDamage(float damage)
    {
        damageFloatingPoint -= damage;
        if (damageFloatingPoint <= 0)
        {
            OnDeath?.Invoke(this);
        }
    }
}

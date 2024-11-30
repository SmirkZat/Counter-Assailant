using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image[] hearts;

    int numberOfHearts = 0;
    Health health;

    void Start()
    {
        health = PlayerController.Instance.GetComponent<Health>();
        if (health != null)
        {
            numberOfHearts = health.StartingHealth;
            InitPlayerHearts();
            health.OnDeath += HandleOnDeath;
            health.OnMaxHealthIncreased += RefreshHearts;
        }
    }

    void Update()
    {
        HandleHealthUIStatus();
    }

    void OnDestroy()
    {
        health.OnDeath -= HandleOnDeath;
        health.OnMaxHealthIncreased -= RefreshHearts;
    }

    void HandleOnDeath(Health sender)
    {
        HandleHealthUIStatus();
    }

    void RefreshHearts(Health sender)
    {
        numberOfHearts = sender.StartingHealth;
        InitPlayerHearts();
    }

    void InitPlayerHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void HandleHealthUIStatus()
    {
        for (int i = 0; i < numberOfHearts; i++)
        {
            if (i < health.CurrentHealth)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }
}

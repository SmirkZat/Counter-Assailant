using System;
using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public Action OnKnockBackStart;
    public Action OnKnockBackEnd;

    [SerializeField] float knockBackTime = .2f;
    public bool isKnockedBack = false;

    Vector2 hitDirection;
    float knockBackThrust;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        OnKnockBackStart += ApplyKnockBackForce;
        OnKnockBackEnd += StopKnockBackRoutine;
    }

    void OnDisable()
    {
        OnKnockBackStart -= ApplyKnockBackForce;
        OnKnockBackEnd -= StopKnockBackRoutine;
    }

    public void GetKnockBack(Vector2 direction, float thrust)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.left;
        hitDirection = direction;
        knockBackThrust = thrust;

        OnKnockBackStart?.Invoke();
    }

    void ApplyKnockBackForce()
    {
        Vector2 force = hitDirection.normalized * knockBackThrust;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());

        StartCoroutine(KnockBackRoutine());
    }

    IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        OnKnockBackEnd?.Invoke();
    }

    void StopKnockBackRoutine()
    {
        isKnockedBack = false;
        rb.velocity = Vector2.zero;
    }
}

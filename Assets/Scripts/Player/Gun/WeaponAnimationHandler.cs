using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationHandler : MonoBehaviour
{
    readonly int SHOOT_ANIMATION = Animator.StringToHash("Shoot");

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Gun.OnShoot += HandleShootAnimation;
    }

    void OnDisable()
    {
        Gun.OnShoot -= HandleShootAnimation;
    }

    void HandleShootAnimation(Gun sender)
    {
        if (sender.CurrentAmmo > 0)
        {
            animator.Play(SHOOT_ANIMATION, 0, 0f);
        }
    }

}

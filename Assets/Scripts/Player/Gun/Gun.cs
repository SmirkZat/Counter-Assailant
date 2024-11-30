using System;
using Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Action<Gun> OnShoot;

    public WeaponSO.WeaponType WeaponType => weaponSO.weaponType;
    public int AmmoCapacity => weaponSO.ammo.maxCapacity;

    public int CurrentAmmo
    {
        get
        {
            if (ammoManager == null) return 0;
            return ammoManager.GetCurrentAmmoCount(weaponSO.ammo);
        }
    }

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Bullet bullet;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] int bulletsNumberPerShoot = 1;
    [SerializeField] WeaponSO weaponSO;


    CinemachineImpulseSource impulseSource;
    AmmoManager ammoManager;
    Vector2 mousePos;
    float lastFireTime = 0f;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        ammoManager = GameManager.Instance.AmmoManager;
    }

    void Update()
    {
        if (PlayerController.Instance.IsControlsEnabled())
        {
            RotateGun();
            Shoot();
        }
    }

    void RotateGun()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = PlayerController.Instance.transform.InverseTransformPoint(mousePos);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= lastFireTime)
        {
            // PlayMuzzleFlashVFX();
            OnShoot?.Invoke(this);
            ProcessShooting();
            ResetLastFireTime();
        }
    }

    void ProcessShooting()
    {
        if (CurrentAmmo > 0)
        {
            if (bulletsNumberPerShoot > 1)
            {
                ShootMultiple();
            }
            else
            {
                ShootProjectile();
            }
        }
        impulseSource.GenerateImpulse();
        ReduceCurrentAmmo();
    }

    void ReduceCurrentAmmo()
    {
        if (weaponSO.weaponType != WeaponSO.WeaponType.Pistol)
        {
            // currentAmmo -= 1;
            ammoManager.UseAmmo(weaponSO.ammo, 1);
        }
    }

    void ShootProjectile()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        newBullet.Init(this, (Vector2)bulletSpawnPoint.position, mousePos, weaponSO.damage);
    }

    void ShootMultiple()
    {
        Vector2 direction = (mousePos - (Vector2)bulletSpawnPoint.position).normalized;
        float angleStep = bulletsNumberPerShoot * 2;
        float startAngle = -(bulletsNumberPerShoot - 1) * angleStep / 2;
        float distanceBetweenSpawnPoint = .5f;

        for (int i = 0; i < bulletsNumberPerShoot; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            Vector2 directionRotation = Quaternion.Euler(0, 0, currentAngle) * direction;
            Vector2 spawnPosition = (Vector2)bulletSpawnPoint.position + directionRotation.normalized * distanceBetweenSpawnPoint;
            Bullet newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);
            newBullet.Init(this, spawnPosition, spawnPosition + directionRotation, weaponSO.damage);
        }
    }

    void ResetLastFireTime()
    {
        lastFireTime = Time.time + weaponSO.fireRate;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (mousePos != Vector2.zero)
        {
            Gizmos.DrawLine(transform.position, mousePos);
        }
    }


    // FIXME - Muzzle flash VFX not working when flipping character
    // void PlayMuzzleFlashVFX()
    // {
    //     // muzzleFlashVFX.main.startRotation = Quaternion.Euler(0, 0, angle);
    //     // muzzleFlashVFX.startRotation = angle;
    //     muzzleFlashVFX.Play();
    // }
}

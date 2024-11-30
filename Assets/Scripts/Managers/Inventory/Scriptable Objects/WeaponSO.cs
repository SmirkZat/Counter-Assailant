using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Items/New Weapon", order = 3)]
public class WeaponSO : ItemSO
{
    public enum WeaponType
    {
        Pistol,
        Shotgun,
        Rifle,
        RPG,
    }

    public float initialFireRate;
    public float initialDamage;

    public WeaponType weaponType;
    public float fireRate;
    public AmmoSO ammo;
    public float damage;


    public override void Init()
    {
        itemType = ItemType.Weapon;
        fireRate = initialFireRate;
        damage = initialDamage;
        ammo.Init();
        amount = 1;
    }

}

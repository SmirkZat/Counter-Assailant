using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Upgrade", menuName = "Scriptable Objects/Upgrades/Weapon Upgrade")]
public class WeaponUpgradeSO : UpgradeSO
{
    public WeaponSO weaponSO;

    public void Init(WeaponSO weaponSO)
    {
        fireRateUpgrade.itemId = weaponSO.itemId;
        ammoCapacityUpgrade.itemId = weaponSO.itemId;
        damageUpgrade.itemId = weaponSO.itemId;
    }

    public Upgrade fireRateUpgrade;
    public Upgrade ammoCapacityUpgrade;
    public Upgrade damageUpgrade;
}

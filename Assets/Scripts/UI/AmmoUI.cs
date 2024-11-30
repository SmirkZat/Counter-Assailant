using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;

    Gun selectedWeapon;

    WeaponSwitchingManager weaponSwitchingManager;

    void Start()
    {
        weaponSwitchingManager = PlayerController.Instance.GetComponentInChildren<WeaponSwitchingManager>();
    }

    void Update()
    {
        if (UpdateSelectedWeapon())
        {
            selectedWeapon = weaponSwitchingManager.selectedWeaponGO;
        }
        SetAmmoStateUI();
    }

    bool UpdateSelectedWeapon()
    {
        return (selectedWeapon == null && weaponSwitchingManager.selectedWeaponGO != null)
         || selectedWeapon != weaponSwitchingManager.selectedWeaponGO;
    }

    void SetAmmoStateUI()
    {
        if (selectedWeapon == null) return;

        if (selectedWeapon.WeaponType == WeaponSO.WeaponType.Pistol)
        {
            // TODO change to infinity sing or something else to indicate for infinite ammo
            ammoText.text = "00/00";
        }
        else
        {
            ammoText.text = $"{selectedWeapon.CurrentAmmo}/{selectedWeapon.AmmoCapacity}";
        }
    }
}

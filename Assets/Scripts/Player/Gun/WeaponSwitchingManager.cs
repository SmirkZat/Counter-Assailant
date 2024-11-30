using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchingManager : MonoBehaviour
{
    public static Action<WeaponSwitchingManager> OnWeaponSelectChange;

    public WeaponSO.WeaponType SelectedWeapon => selectedWeapon;
    public Gun selectedWeaponGO { get; private set; }

    InventoryManager inventory;
    List<WeaponSO> weapons;

    WeaponSO.WeaponType selectedWeapon;

    void Start()
    {
        inventory = GameManager.Instance.Inventory;
        if (inventory != null)
        {
            GetWeaponsFromInventory();
        }

        SelectWeapon();
    }

    void OnEnable()
    {
        // Refetch all weapons when new weapon is bought to be sync with the inventory
        ShopManager.OnNewWeaponBought += GetWeaponsFromInventory;
    }

    void OnDisable()
    {
        ShopManager.OnNewWeaponBought -= GetWeaponsFromInventory;
    }

    void Update()
    {
        WeaponSO.WeaponType previousSelectedWeapon = selectedWeapon;

        HandleInputs();

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void GetWeaponsFromInventory()
    {
        weapons = inventory.GetAllWeapons();
    }

    void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count >= 1)
        {
            selectedWeapon = weapons[0].weaponType;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && weapons.Count >= 2)
        {
            selectedWeapon = weapons[1].weaponType;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && weapons.Count >= 3)
        {
            selectedWeapon = weapons[2].weaponType;
        }
        // else if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4) // TBD Rocket Launcher
        // {
        //     selectedWeapon = 3;
        // }
    }

    void SelectWeapon()
    {
        // Select weapon according to the itemType in the inventory
        foreach (Transform weapon in transform)
        {
            WeaponSO.WeaponType weaponType = weapon.GetComponent<Gun>().WeaponType;
            if (weaponType == selectedWeapon)
            {
                selectedWeaponGO = weapon.gameObject.GetComponent<Gun>();
                weapon.gameObject.SetActive(true);
                OnWeaponSelectChange?.Invoke(this);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}

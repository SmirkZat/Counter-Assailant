using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    const string UPGRADE_MAX_HEALTH = "health";
    const string UPGRADE_FIRE_RATE = "firerate";
    const string UPGRADE_DAMAGE = "damage";
    const string UPGRADE_AMMO_CAPACITY = "ammo";

    [SerializeField] PlayerUpgradeSO playerUpgradeSO;
    [SerializeField] List<WeaponUpgradeSO> weaponUpgradeSOs;
    [SerializeField] UpgradesUIManager upgradesUIManager;

    [SerializeField] Canvas upgradesMenu;

    InventoryManager inventory;
    AmmoManager ammoManager;
    Health playerHealth;
    Dictionary<string, Upgrade> selectedUpgrades = new Dictionary<string, Upgrade>();

    void Start()
    {
        inventory = GameManager.Instance.Inventory;
        ammoManager = GameManager.Instance.AmmoManager;
        playerHealth = PlayerController.Instance.GetComponent<Health>();
        SetupUpgradeList();
        HideUpgradesMenu();
    }

    void OnEnable()
    {
        LevelManager.OnLevelCompleted += ShowUpgradesMenu;
    }

    void OnDisable()
    {
        LevelManager.OnLevelCompleted -= ShowUpgradesMenu;
    }

    void ShowUpgradesMenu()
    {
        upgradesMenu.gameObject.SetActive(true);
        UIManager.Instance.ToggleCursorCrosshair(true);
        upgradesUIManager.SetHealthUpgradeDetails(playerUpgradeSO);
        foreach (KeyValuePair<string, Upgrade> upg in selectedUpgrades)
        {
            upgradesUIManager.SetWeaponUpgradeDetails(upg.Key, upg.Value);
        }
    }

    void HideUpgradesMenu()
    {
        upgradesMenu.gameObject.SetActive(false);
    }

    void SetupUpgradeList()
    {
        SetAvailableWeapons();
    }

    void SetAvailableWeapons()
    {
        List<WeaponUpgradeSO> availableWeaponsToUpgrade = new List<WeaponUpgradeSO>();
        WeaponUpgradeSO pistol = null;
        foreach (WeaponUpgradeSO weaponUpgrade in weaponUpgradeSOs)
        {
            weaponUpgrade.Init(weaponUpgrade.weaponSO);
            if (weaponUpgrade.weaponSO.weaponType == WeaponSO.WeaponType.Pistol)
            {
                pistol = weaponUpgrade;
            }
            else if (inventory.InventoryItems.ContainsKey(weaponUpgrade.weaponSO.itemId) && weaponUpgrade.weaponSO.weaponType != WeaponSO.WeaponType.Pistol)
            {
                availableWeaponsToUpgrade.Add(weaponUpgrade);
            }
        }
        // check if there are more weapons except for the pistol
        // pistol has infinite amount of bullets
        if (availableWeaponsToUpgrade.Count >= 1)
        {
            selectedUpgrades.Add(UPGRADE_AMMO_CAPACITY, GetRandomWeapon(availableWeaponsToUpgrade).ammoCapacityUpgrade);
        }
        if (pistol != null)
        {
            availableWeaponsToUpgrade.Add(pistol);
        }
        selectedUpgrades.Add(UPGRADE_FIRE_RATE, GetRandomWeapon(availableWeaponsToUpgrade).fireRateUpgrade);
        selectedUpgrades.Add(UPGRADE_DAMAGE, GetRandomWeapon(availableWeaponsToUpgrade).damageUpgrade);
    }

    WeaponUpgradeSO GetRandomWeapon(List<WeaponUpgradeSO> upgrades)
    {
        if (upgrades.Count == 0) return null;
        int randomIndex = Random.Range(0, upgrades.Count);
        return upgrades[randomIndex];
    }

    public void HandleSelectUpgrade(string upgradeType)
    {
        if (upgradeType == UPGRADE_MAX_HEALTH)
        {
            IncreasePlayersHealth();
        }
        else
        {
            UpgradeWeapon(upgradeType, selectedUpgrades[upgradeType]);
        }
    }

    void UpgradeWeapon(string upgradeType, Upgrade upgrade)
    {
        if (upgrade.price > inventory.Gems) return;

        string weaponId = selectedUpgrades[upgradeType].itemId;
        WeaponSO weapon = (WeaponSO)inventory.GetItemFromInventory(weaponId);
        switch (upgradeType)
        {
            case UPGRADE_DAMAGE:
                weapon.damage += upgrade.value;
                break;
            case UPGRADE_FIRE_RATE:
                weapon.fireRate -= upgrade.value;
                break;
            case UPGRADE_AMMO_CAPACITY:
                ammoManager.IncreaseAmmoCapacity(weapon.ammo, (int)upgrade.value);
                break;
        }
        inventory.DecreaseGemsAmount(upgrade.price);
        upgradesUIManager.HideSlot(upgrade.upgradeType);
    }

    public void GoToShop()
    {
        GameManager.Instance.GoToStore();
    }

    public void GoToNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void IncreasePlayersHealth()
    {
        if (playerHealth != null && playerUpgradeSO.maxHealthUpgrade.price <= inventory.Gems)
        {
            inventory.DecreaseGemsAmount(playerUpgradeSO.maxHealthUpgrade.price);
            playerHealth.IncreaseMaxHealth((int)playerUpgradeSO.maxHealthUpgrade.value);
        }
        upgradesUIManager.HideSlot(Upgrade.UpgradeType.Health);
    }
}

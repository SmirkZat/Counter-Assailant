using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static Action OnNewWeaponBought;
    const string SHOP_ITEM_WEAPON = "Weapon";
    const string SHOP_ITEM_MEDKIT = "Medkit";
    const string SHOP_ITEM_AMMUNITION = "Ammunition";

    [Header("Shop content")]
    [SerializeField] List<ShopItemSO> shopContent;

    InventoryManager inventory;
    AmmoManager ammoManager;
    ShopUI shopUI;
    Dictionary<string, ShopItemSO> shopList = new Dictionary<string, ShopItemSO>();

    void Awake()
    {
        shopUI = GetComponent<ShopUI>();
    }

    void Start()
    {
        inventory = GameManager.Instance.Inventory;
        ammoManager = GameManager.Instance.AmmoManager;
        InitStoreContent();
        RefreshSlotsPurchaseStatus();
    }

    void InitStoreContent()
    {
        List<ShopItemSO> weapons = new List<ShopItemSO>();
        foreach (ShopItemSO shopItemSO in shopContent)
        {
            if (shopItemSO.itemType == ItemSO.ItemType.Ammunition)
            {
                shopList.TryAdd(SHOP_ITEM_AMMUNITION, shopItemSO);
                shopUI.SetAmmoSlotDetails(shopList[SHOP_ITEM_AMMUNITION], CanBuyAmmo());
            }
            if (shopItemSO.itemType == ItemSO.ItemType.MedKit)
            {
                shopList.TryAdd(SHOP_ITEM_MEDKIT, shopItemSO);
                shopUI.SetMedkitSlotDetails(shopList[SHOP_ITEM_MEDKIT], true);
            }
            else if (shopItemSO.itemType == ItemSO.ItemType.Weapon && !IsExistsInInventory(shopItemSO.item.itemId))
            {
                weapons.Add(shopItemSO);
            }
        }
        bool weaponInShop = weapons.Count > 0;
        if (!weaponInShop)
        {
            shopUI.HideSlot(ItemSO.ItemType.Weapon);
        }
        else
        {
            int randomWeaponIndex = UnityEngine.Random.Range(0, weapons.Count);
            shopList.TryAdd(SHOP_ITEM_WEAPON, weapons[randomWeaponIndex]);
            shopUI.SetWeaponSlotDetails(shopList[SHOP_ITEM_WEAPON], weaponInShop);
        }
    }

    void RefreshSlotsPurchaseStatus()
    {
        foreach (KeyValuePair<string, ShopItemSO> shopItem in shopList)
        {
            if (shopItem.Value.price > inventory.Coins && shopItem.Value)
            {
                shopUI.ShowNoCoinsOverlay(shopItem.Value.itemType);
            }
        }
    }

    void HideItemSlot(ItemSO.ItemType itemType)
    {
        shopUI.HideSlot(itemType);
    }

    bool IsExistsInInventory(string itemId)
    {
        return inventory.InventoryItems.ContainsKey(itemId);
    }

    bool CanBuyAmmo()
    {
        return inventory.GetAllWeapons().Count > 1;
    }

    public void PurchaseItem(string itemType)
    {
        if (shopList[itemType].price > inventory.Coins)
        {
            Debug.Log("Not enough coins to purchase");
            return;
        }
        if (itemType == SHOP_ITEM_AMMUNITION)
        {
            HandleAmmoPurchase();
        }
        else
        {
            HandleItemPurchase(itemType);
        }
        HideItemSlot(shopList[itemType].itemType);
        inventory.DecreaseCoinsAmount(shopList[itemType].price);
        RefreshSlotsPurchaseStatus();
    }

    void HandleItemPurchase(string itemType)
    {
        if (!inventory.AddItem(shopList[itemType].item))
        {
            Debug.Log("Item already exists in inventory");
        }
        else if (itemType == SHOP_ITEM_WEAPON)
        {
            OnNewWeaponBought?.Invoke();
        }
    }

    void HandleAmmoPurchase()
    {
        WeaponSO weapon;
        foreach (WeaponSO weaponItem in inventory.GetAllWeapons())
        {
            weapon = inventory.InventoryItems[weaponItem.itemId] as WeaponSO;
            ammoManager.AddAmmo(weapon.ammo, weapon.ammo.maxCapacity);
        }
    }

    public void HandleNextLevelButton()
    {
        GameManager.Instance.LoadNextLevel();
    }
}

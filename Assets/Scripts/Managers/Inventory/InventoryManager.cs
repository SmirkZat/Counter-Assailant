using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, ItemSO> InventoryItems => itemDictionary;
    public int Coins => coins;
    public int Gems => gems;

    [SerializeField] List<ItemSO> itemList = new List<ItemSO>();

    AmmoManager ammoManager;
    Dictionary<string, ItemSO> itemDictionary = new Dictionary<string, ItemSO>();


    int coins = 0;
    int gems = 0;


    void Awake()
    {
        if (ScenePersist.Instance == null)
        {
            Init();
        }
    }

    void OnEnable()
    {
        Coin.OnCoinCollected += HandleCollectCoins;
        Gem.OnGemCollected += HandleCollectGems;
    }

    void OnDisable()
    {
        Coin.OnCoinCollected -= HandleCollectCoins;
        Gem.OnGemCollected -= HandleCollectGems;
    }


    void Init()
    {
        ammoManager = GameManager.Instance.AmmoManager;

        foreach (ItemSO item in itemList)
        {
            AddItem(item);
        }
    }

    public bool AddItem(ItemSO item)
    {
        // check if item already exist
        if (itemDictionary.ContainsKey(item.itemId))
        {
            // only one type of weapon can be in inventory
            // if item not of type of weapon -> increase amount
            if (item.itemType != ItemSO.ItemType.Weapon)
            {
                itemDictionary[item.itemId].amount += 1;
                Debug.Log("Item amount increased, amount: " + itemDictionary[item.itemId].amount);
                return true;
            }
            return false;
        }

        // add new item to inventory
        item.Init();
        itemDictionary.Add(item.itemId, item);
        // if item of type weapon -> set ammo in AmmoManager
        if (item.itemType == ItemSO.ItemType.Weapon)
        {
            AmmoSO weaponAmmo;
            // Add ammo to ammo inventory
            weaponAmmo = (item as WeaponSO).ammo;
            ammoManager.AddAmmo(weaponAmmo, weaponAmmo.maxCapacity);
        }
        return true;
    }

    public void DecreaseItemAmount(ItemSO item)
    {
        if (itemDictionary.ContainsKey(item.itemId) && item.amount > 0)
        {
            item.amount -= 1;
        }
    }



    void HandleCollectCoins(Coin coin)
    {
        IncreaseCoinsAmount(coin.CoinAmount);
    }

    void HandleCollectGems()
    {
        IncreaseGemsAmount(1);
    }

    void IncreaseCoinsAmount(int coinAmount)
    {
        coins += coinAmount;
    }

    public void DecreaseCoinsAmount(int coinAmount)
    {
        if (coins < 0)
        {
            coins = 0;
            return;
        }
        coins -= coinAmount;
    }

    void IncreaseGemsAmount(int gemsAmount)
    {
        gems += gemsAmount;
    }

    public void DecreaseGemsAmount(int gemsAmount)
    {
        if (gems < 0)
        {
            gems = 0;
            return;
        }
        gems -= gemsAmount;
    }

    public ItemSO GetItemFromInventory(string itemId)
    {
        if (!itemDictionary.ContainsKey(itemId))
        {
            return null;
        }
        return itemDictionary[itemId];
    }
    public ItemSO.ItemType GetListItemType(int index)
    {
        return itemList[index].itemType;
    }

    public List<WeaponSO> GetAllWeapons()
    {
        List<WeaponSO> weapons = new List<WeaponSO>();
        foreach (KeyValuePair<string, ItemSO> item in itemDictionary)
        {
            if (item.Value.itemType == ItemSO.ItemType.Weapon)
            {
                weapons.Add(item.Value as WeaponSO);
            }
        }
        return weapons;
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemAssets itemAssets;
    [SerializeField] Transform weaponsPanel;
    [SerializeField] TextMeshProUGUI medkitAmount;


    InventoryManager inventory;
    List<WeaponSO> weapons;


    void Start()
    {
        inventory = GameManager.Instance.Inventory;
        weapons = inventory.GetAllWeapons();
        RefreshInventoryItems();
    }

    void Update()
    {
        if (inventory.InventoryItems.ContainsKey("medkit"))
        {
            medkitAmount.text = inventory.InventoryItems["medkit"].amount.ToString();
        }
    }

    void RefreshInventoryItems()
    {
        int i = 0;

        foreach (Transform slot in weaponsPanel)
        {
            if (i < weapons.Count)
            {
                Image image = slot.Find("Slot Item").GetComponent<Image>();
                image.sprite = itemAssets.GetSprite(weapons[i]);
            }
            i++;
        }
    }
}

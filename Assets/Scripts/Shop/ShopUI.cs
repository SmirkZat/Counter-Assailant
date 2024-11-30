using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Weapon Slot Details")]
    [SerializeField] Transform weaponSlot;
    [SerializeField] TextMeshProUGUI weaponSlotTitle;
    [SerializeField] TextMeshProUGUI weaponSlotPriceText;
    [SerializeField] Image weaponSlotImage;

    [Header("Medkit Slot Details")]
    [SerializeField] Transform medkitSlot;
    [SerializeField] TextMeshProUGUI medkitSlotTitle;
    [SerializeField] TextMeshProUGUI medkitSlotPriceText;
    [SerializeField] Image medkitSlotImage;

    [Header("Ammunition Slot Details")]
    [SerializeField] Transform ammoSlot;
    [SerializeField] TextMeshProUGUI ammoSlotTitle;
    [SerializeField] TextMeshProUGUI ammoSLotPriceText;
    [SerializeField] Image ammoSlotImage;
    [SerializeField] RectTransform overlayPrefab;
    [SerializeField] TextMeshProUGUI NextLevelText;

    void Start()
    {
        NextLevelText.text = UIManager.Instance.GetNextLevelText();
    }
    public void SetMedkitSlotDetails(ShopItemSO shopItemSO, bool isActive)
    {
        if (!isActive)
        {
            HideSlot(ItemSO.ItemType.MedKit);
        }
        medkitSlotTitle.text = shopItemSO.title;
        medkitSlotPriceText.text = shopItemSO.price.ToString();
        medkitSlotImage.sprite = shopItemSO.itemSprite;
    }
    public void SetWeaponSlotDetails(ShopItemSO shopItemSO, bool isActive)
    {
        if (!isActive)
        {
            HideSlot(ItemSO.ItemType.Weapon);
        }
        weaponSlotTitle.text = shopItemSO.title;
        weaponSlotPriceText.text = shopItemSO.price.ToString();
        weaponSlotImage.sprite = shopItemSO.itemSprite;
    }
    public void SetAmmoSlotDetails(ShopItemSO shopItemSO, bool isActive)
    {
        if (!isActive)
        {
            HideSlot(ItemSO.ItemType.Ammunition);
        }
        ammoSlotTitle.text = shopItemSO.title;
        ammoSLotPriceText.text = shopItemSO.price.ToString();
        ammoSlotImage.sprite = shopItemSO.itemSprite;
    }

    public void ShowNoCoinsOverlay(ItemSO.ItemType itemType)
    {
        Transform slot = GetSlotByType(itemType);
        Transform overlay = slot.Find("Overlay");
        if (overlay == null && slot.gameObject.activeInHierarchy)
        {
            RectTransform instance = Instantiate(overlayPrefab, slot.position, Quaternion.identity, slot);
            instance.name = "Overlay";
            instance.anchoredPosition = Vector2.zero;
        }
    }

    public void HideSlot(ItemSO.ItemType itemType)
    {
        Transform slot = GetSlotByType(itemType);
        if (slot != null)
        {
            slot.gameObject.SetActive(false);
        }
    }

    Transform GetSlotByType(ItemSO.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemSO.ItemType.MedKit:
                return medkitSlot;
            case ItemSO.ItemType.Weapon:
                return weaponSlot;
            case ItemSO.ItemType.Ammunition:
                return ammoSlot;
        }
        return null;
    }
}

using UnityEngine;

public class UpgradesUIManager : MonoBehaviour
{
    [SerializeField] UITextGroup uITextGroup;
    [SerializeField] Transform healthUpgradeSlot;
    [SerializeField] Transform fireRateUpgradeSlot;
    [SerializeField] Transform damageUpgradeSlot;
    [SerializeField] Transform ammoUpgradeSlot;


    void Awake()
    {
        uITextGroup.Init();
        HideSlot(Upgrade.UpgradeType.AmmoCapacity);
    }

    void Start()
    {
        uITextGroup.SetText("btn_next_level", UIManager.Instance.GetNextLevelText());
    }


    public void SetHealthUpgradeDetails(PlayerUpgradeSO playerUpgrade)
    {
        string description = playerUpgrade.maxHealthUpgrade.description;
        string price = $"{playerUpgrade.maxHealthUpgrade.price}";
        SetSlotDetails("health", playerUpgrade.maxHealthUpgrade.title, description, price);
    }

    public void SetWeaponUpgradeDetails(string prefix, Upgrade upgrade)
    {
        if (upgrade.upgradeType == Upgrade.UpgradeType.AmmoCapacity)
        {
            ammoUpgradeSlot.gameObject.SetActive(true);
        }

        string title = upgrade.title;
        string description = upgrade.description;
        string price = $"{upgrade.price}";
        SetSlotDetails(prefix, title, description, price);
    }

    void SetSlotDetails(string prefix, string title, string description, string price)
    {
        uITextGroup.SetText($"{prefix}_title", title);
        uITextGroup.SetText($"{prefix}_description", description);
        uITextGroup.SetText($"{prefix}_price", price);
    }

    public void HideSlot(Upgrade.UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case Upgrade.UpgradeType.Health:
                healthUpgradeSlot.gameObject.SetActive(false);
                break;
            case Upgrade.UpgradeType.FireRate:
                fireRateUpgradeSlot.gameObject.SetActive(false);
                break;
            case Upgrade.UpgradeType.Damage:
                damageUpgradeSlot.gameObject.SetActive(false);
                break;
            case Upgrade.UpgradeType.AmmoCapacity:
                ammoUpgradeSlot.gameObject.SetActive(false);
                break;
        }
    }
}

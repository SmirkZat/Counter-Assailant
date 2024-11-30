using System;

[Serializable]
public class Upgrade
{
    public enum UpgradeType
    {
        Health,
        FireRate,
        Damage,
        AmmoCapacity
    }
    public UpgradeType upgradeType;
    public string itemId;
    public float value;
    public string title;
    public string description;
    public int price;
}

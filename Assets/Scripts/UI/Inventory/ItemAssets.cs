using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public Sprite gunSprite;
    public Sprite shotgunSprite;
    public Sprite rifleSprite;
    public Sprite Medkit;


    public Sprite GetSprite(ItemSO item)
    {
        if (item.itemType == ItemSO.ItemType.MedKit)
        {
            return Medkit;
        }
        else if (item.itemType == ItemSO.ItemType.Weapon)
        {
            return GetWeaponSprite(item as WeaponSO);
        }
        return null;
    }

    public Sprite GetWeaponSprite(WeaponSO weapon)
    {
        switch (weapon.weaponType)
        {
            default:
            case WeaponSO.WeaponType.Pistol:
                return gunSprite;
            case WeaponSO.WeaponType.Shotgun:
                return shotgunSprite;
            case WeaponSO.WeaponType.Rifle:
                return rifleSprite;
        }
    }
}

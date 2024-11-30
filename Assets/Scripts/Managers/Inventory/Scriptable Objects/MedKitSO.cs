using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Items/New MedKit", order = 4)]
public class MedKitSO : ItemSO
{
    public int healthRestoration;
    public bool restoreToFullHealth;

    public override void Init()
    {
        itemType = ItemType.MedKit;
        amount = 1;
    }


}

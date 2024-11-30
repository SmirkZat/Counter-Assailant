using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition", menuName = "Scriptable Objects/Items/New Ammunition", order = 5)]
public class AmmoSO : ItemSO
{
    public int initialCapacity;
    public int maxCapacity;

    public override void Init()
    {
        itemType = ItemType.Ammunition;
        maxCapacity = initialCapacity;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Scriptable Objects/New Shop Item", order = 2)]
public class ShopItemSO : ScriptableObject
{
    public ItemSO.ItemType itemType;
    public Sprite itemSprite;
    public ItemSO item;
    public string title;
    public string description;
    public int price;
}

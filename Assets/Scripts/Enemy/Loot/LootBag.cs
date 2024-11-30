using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] List<Loot> lootList = new List<Loot>();

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("NO LOOT DROPPED");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition, Transform lootParentTransform)
    {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            Instantiate(droppedItem.lootPrefab, spawnPosition, Quaternion.identity, lootParentTransform);
        }
    }

}

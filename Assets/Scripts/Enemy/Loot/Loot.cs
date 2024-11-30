using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Scriptable Objects/Loot", order = 1)]
public class Loot : ScriptableObject
{
    public GameObject lootPrefab;
    public string lootName;
    public int dropChance;
}

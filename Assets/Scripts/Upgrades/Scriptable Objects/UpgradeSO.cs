using UnityEngine;

public class UpgradeSO : ScriptableObject
{
    public enum EntityType
    {
        Player,
        Weapon
    }
    public EntityType entityType;
}

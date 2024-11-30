using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    Dictionary<AmmoSO, int> ammoInventory = new Dictionary<AmmoSO, int>();

    public void IncreaseAmmoCapacity(AmmoSO ammo, int capacity)
    {
        if (ammoInventory.ContainsKey(ammo))
        {
            ammo.maxCapacity += capacity;
            AddAmmo(ammo, capacity);
        }
    }

    public void SetAmmoCapacity(AmmoSO ammo, int capacity)
    {
        if (ammoInventory.ContainsKey(ammo))
        {
            ammo.maxCapacity = capacity;
        }
    }

    public void AddAmmo(AmmoSO ammo, int amount)
    {
        if (!ammoInventory.ContainsKey(ammo))
        {
            ammoInventory[ammo] = 0;
        }

        int ammoCount = GetCurrentAmmoCount(ammo);
        if ((ammoCount + amount) >= ammo.maxCapacity)
        {
            ammoInventory[ammo] = ammo.maxCapacity;
            return;
        }
        ammoInventory[ammo] += amount;
    }

    public bool UseAmmo(AmmoSO ammo, int amount)
    {
        int ammoCount = GetCurrentAmmoCount(ammo);
        if (ammoInventory.ContainsKey(ammo) && ammoCount >= amount)
        {
            ammoInventory[ammo] -= amount;
            return true;
        }
        return false; // incase of empty clip make empty clip sound
    }

    public int GetCurrentAmmoCount(AmmoSO ammo)
    {
        if (ammoInventory.ContainsKey(ammo))
        {
            return ammoInventory[ammo];
        }
        return 0;
    }
}

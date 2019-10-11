using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopSlot[] products;
    public Inventory connectedInventory;

    void Start()
    {
        connectedInventory = FindObjectOfType<Inventory>();
        Invoke("InitialUpdate", 0.2f);
    }

    private void InitialUpdate()
    {
        foreach (ShopSlot slot in products)
        {
            slot.UpdateAmount();
        }
    }

    public void AddToShop(TroopType troopType)
    {
        foreach (ShopSlot slot in products)
        {
            if (slot.troopType == troopType)
            {
                slot.amount++;
                slot.UpdateAmount();
                break;
            }
        }
    }

    public int GetValue(TroopType troopType)
    {
        foreach (ShopSlot slot in products)
        {
            if (slot.troopType == troopType)
            {
                return slot.value;
            }
        }
        return 0;
    }
}
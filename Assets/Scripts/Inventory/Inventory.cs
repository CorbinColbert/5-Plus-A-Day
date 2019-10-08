using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public ShopSlot[] inventoryShopSlots;
    public List<TroopType> deployed;
    public InventorySlot selectedSlot;

    void Start() {
        deployed = new List<TroopType>();
    }

    public void Add(TroopType troopType) 
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.troopType == troopType)
            {
                slot.amount++;
                slot.UpdateAmount();
                break;
            }
        }
        foreach (ShopSlot slot in inventoryShopSlots)
        {
            if (slot.troopType == troopType)
            {
                slot.amount++;
                slot.UpdateAmount();
                break;
            }
        }
    }

    public void RemoveTemporary(TroopType troopType)
    {   deployed.Add(troopType);
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.troopType == troopType)
            {
                slot.amount++;
                slot.UpdateAmount();
                break;
            }
        }
        foreach (ShopSlot slot in inventoryShopSlots)
        {
            if (slot.troopType == troopType)
            {
                slot.amount++;
                slot.UpdateAmount();
                break;
            }
        }
    }

    public void RemovePermanant(TroopType troopType)
    {   foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.troopType == troopType)
            {
                slot.amount--;
                slot.UpdateAmount();
                break;
            }
        }
        foreach (ShopSlot slot in inventoryShopSlots)
        {
            if (slot.troopType == troopType)
            {
                slot.amount--;
                slot.UpdateAmount();
                break;
            }
        }
    }

    public int Count(TroopType troopType)
    {   int count = 0;
        foreach (InventorySlot slot in inventorySlots)
            if (slot.troopType == troopType)
                count++;
        
        return count;
    }

}

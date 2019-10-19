using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopSlot[] products;
    public Inventory inventory;
    private Dictionary<TroopType, ShopSlot> shopDict;

    void Start() {
        // Dictionary of shop slots
        shopDict = new Dictionary<TroopType, ShopSlot>();
        foreach (ShopSlot slot in products)
        {
            shopDict.Add(slot.troopType, slot);
        }

        Invoke("UpdateAll", 0.1f);
    }

    private void UpdateAll() {
        foreach (ShopSlot slot in products)
        {   // Update text
            slot.UpdateAmount();

            // Sync buy and sell value for this slot
            // Prices only set in sell slots on the editor
            inventory.shopDict[slot.troopType].value = slot.value;
        }
    }

    public void AddToShop(TroopType troopType)
    {
        ShopSlot sS = shopDict[troopType];
        sS.amount++;
        sS.UpdateAmount();
    }

    public int GetValue(TroopType troopType)
    {
        return shopDict[troopType].value;    
    }
}
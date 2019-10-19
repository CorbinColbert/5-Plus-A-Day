using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public ShopSlot[] inventoryShopSlots;
    
    public Dictionary<TroopType, InventorySlot> invDict;
    public Dictionary<TroopType, ShopSlot> shopDict;

    public List<TroopType> deployed;
    public InventorySlot selectedSlot;

    void Start() {
        //List of troops which are fighting, add them back one by one after the fight
        deployed = new List<TroopType>();

        //Dictionary of inventory slots
        invDict = new Dictionary<TroopType, InventorySlot>();
        foreach (InventorySlot slot in inventorySlots)
        {
            invDict.Add(slot.troopType, slot);
        }
            

        //Dictionary of shop slots
        shopDict = new Dictionary<TroopType, ShopSlot>();
        foreach (ShopSlot slot in inventoryShopSlots)
        {
            shopDict.Add(slot.troopType, slot);
        }
            
    }

    public void Add(TroopType troopType) 
    {
        InventorySlot iS = invDict[troopType];
        iS.amount++;
        iS.UpdateAmount();

        ShopSlot sS = shopDict[troopType];
        sS.amount++;
        sS.UpdateAmount();
    }

    //For when troops are placed on the board
    public void RemoveTemporary(TroopType troopType)
    {   
        deployed.Add(troopType);
        Remove(troopType);
    }

    //For when troops are sold
    public void Remove(TroopType troopType)
    {
        InventorySlot iS = invDict[troopType];
        iS.amount--;
        iS.UpdateAmount();

        ShopSlot sS = shopDict[troopType];
        sS.amount--;
        sS.UpdateAmount();
    }

    //For when a battle is over
    public void RestoreTroops()
    {
        foreach (TroopType troop in deployed)
        {
            Add(troop);
        }
    }

    //How many troops of that type do you have in the inventory
    public int Count(TroopType troopType)
    {
        return invDict[troopType].amount;
    }

}

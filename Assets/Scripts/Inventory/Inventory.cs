using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static List<TroopType> deployed;

    public static InventorySlot carrotSlot;
    public static InventorySlot pearSlot;
    public static InventorySlot tangerineSlot;
    public static InventorySlot orangeSlot;
    public static InventorySlot bananaSlot;
    public static InventorySlot grapeSlot;
    public static InventorySlot cherrySlot;
    public static InventorySlot lemonSlot;
    public static InventorySlot peachSlot;
    public static InventorySlot radishSlot;
    public static InventorySlot pepperSlot;
    public static InventorySlot cakeSlot;

    public InventorySlot selectedSlot;

    public void AddTroopToInventory(TroopType Type)
    {
        switch (Type)
        {
            case TroopType.CARROT:
                carrotSlot.amount++;
                break;
            case TroopType.PEAR:
                pearSlot.amount++;
                break;
            case TroopType.TANGERINE:
                break;
            case TroopType.ORANGE:
                break;
            case TroopType.BANANA:
                break;
            case TroopType.GRAPE:
                break;
            case TroopType.CHERRY:
                break;
            case TroopType.LEMON:
                break;
            case TroopType.PEACH:
                break;
            case TroopType.RADISH:
                break;
            case TroopType.PEPPER:
                break;
            case TroopType.CAKE:
                break;
        }
    }

    public void RecallToInventory()
    {
        foreach (TroopType t in deployed)
        {
            AddTroopToInventory(t);
        }
        deployed.Clear();
    }

    public void PlaceOnBoard(TroopType Type)
    {
        deployed.Add(Type);
    }

    void OnDisable()
    {
        selectedSlot = null;
    }
}

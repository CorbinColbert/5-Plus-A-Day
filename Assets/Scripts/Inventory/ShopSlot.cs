using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Shop shop {get; private set;}
    public TroopType troopType;
    private Text text;
    private Button button;
    public int amount;
    public int value;
    public bool isInventory;

    private void Start()
    {
        //Shop is singleton class
        shop = FindObjectOfType<Shop>();

        //One text and button are required to be children on this gameObject
        text = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();

        if (isInventory) {
            button.onClick.AddListener(Sell);
        } else {
            button.onClick.AddListener(Buy);
        }
    }

    //Call this when purchasing a troop from the shop
    public void Buy()
    {
        if (GameManager.currency - value < 0 || amount < 1)
        {
            //Cannot afford the item or there are none left
            return;
        }

        shop.inventory.Add(troopType);
        GameManager.currency -= shop.GetValue(troopType);
        amount--;
        UpdateAmount();
    }

    //Call this when selling a troop from your inventory in the shop
    public void Sell() {
        if (amount < 1)
        {
            return;
        }

        shop.inventory.Remove(troopType);
        shop.AddToShop(troopType);
        GameManager.currency += shop.GetValue(troopType);
        if (!isInventory)
        {
            amount++;
        }
        
        UpdateAmount();
    }

    //Updates attatched text component
    public void UpdateAmount()
    {
        if (text != null)
        {
            text.text = "" + amount;
        }
    }
}

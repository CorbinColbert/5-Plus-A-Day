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
        shop = FindObjectOfType<Shop>();
        text = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();

        if (isInventory) {
            button.onClick.AddListener(Sell);
        } else {
            button.onClick.AddListener(Buy);
        }
    }

    void Update() {
        
    }

    public void Buy()
    {
        print("Buy called on "+gameObject.name);

        if (GameManager.currency - value < 0 || amount < 1)
        {
            //Cannot afford the item or there are none left
            return;
        }

        shop.connectedInventory.Add(troopType);
        GameManager.currency -= shop.GetValue(troopType);
        amount--;
        UpdateAmount();
    }

    public void Sell() {
        print("Sell called on "+gameObject.name);

        if (amount < 1)
        {
            return;
        }

        shop.connectedInventory.RemovePermanant(troopType);
        shop.AddToShop(troopType);
        GameManager.currency += shop.GetValue(troopType);
        if (!isInventory)
        {
            amount++;
        }
        
        UpdateAmount();
    }

    public void UpdateAmount()
    {
        if (text != null)
        {
            text.text = "" + amount;
        }
    }
}

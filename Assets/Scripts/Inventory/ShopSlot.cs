using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public TroopType Type;
    public int stock;
    public GameObject stockTextObject;
    public int cost;

    private void Start()
    {
        UpdateStockText();
        GetComponentInChildren<Button>().onClick.AddListener(Buy);
    }

    public void Buy()
    {
        print("Pressed buy on " + Type.ToString());
        if (stock > 0)
        {
            stock--;
            UpdateStockText();
        }
    }

    public void Restock(int amount)
    {
        stock += amount;
        UpdateStockText();
    }

    private void UpdateStockText()
    {
        if (stockTextObject.TryGetComponent<Text>(out Text text))
        {
            text.text = ""+stock;
        }
    }
}

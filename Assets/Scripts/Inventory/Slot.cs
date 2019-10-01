using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public TroopType troopType;
    public GameObject stockTextObject;
    public int stock;
    public int cost;

    private void Start()
    {
        UpdateStockText();
        GetComponentInChildren<Button>().onClick.AddListener(Buy);
    }

    public void Buy()
    {
        print("Buy");

        if (GameManager.currency - cost < 0 || stock < 1)
        {
            return;
        }

        stock--;
        GameManager.currency -= cost;
        UpdateStockText();
    }

    public void Restock(int amount)
    {
        stock += amount;
        UpdateStockText();
    }

    void UpdateStockText()
    {
        if (stockTextObject.TryGetComponent<Text>(out Text text))
        {
            text.text = "" + stock;
        }
    }
}

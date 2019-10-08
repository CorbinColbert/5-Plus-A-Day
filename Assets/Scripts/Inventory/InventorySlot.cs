using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Inventory inventory;
    public TroopType troopType;
    private Text text;
    private Button button;
    public int amount;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();
        inventory = FindObjectOfType<Inventory>();
        button.onClick.AddListener(UpdateAmount);
        button.onClick.AddListener(SetSelected);
        UpdateAmount();
    }

    private void SetSelected() {
        inventory.selectedSlot = this;
    }

    public void UpdateAmount()
    {
        text = GetComponentInChildren<Text>();
        if (text != null)
        {
            text.text = "" + amount;
        }
        else
        {
            print("text not found for "+gameObject.name);
        }
    }
}
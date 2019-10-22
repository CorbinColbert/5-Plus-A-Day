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
    public int amount { get; set; }

    private void Start()
    {
        //Text and buttons for the price to be update for the UI are automatically found,
        //the Slot component should be the parent of one text and one button component
        text = GetComponentInChildren<Text>();
        button = GetComponentInChildren<Button>();

        //Only one inventory in the scene
        inventory = FindObjectOfType<Inventory>();

        //When the button is clicked, update the amount and set selected
        button.onClick.AddListener(UpdateAmount);
        button.onClick.AddListener(SetSelected);
        UpdateAmount();
    }

    private void SetSelected() {
        //Only one slot may be selected at a time
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PriceUpdater : MonoBehaviour
{
    public Shop shop;
    public Text priceLabel;

    void Start()
    {
        AddPointerTriggers(shop.products);
        AddPointerTriggers(shop.inventory.inventoryShopSlots);
    }

    void AddPointerTriggers(ShopSlot[] slots)
    {
        foreach (ShopSlot slot in slots)
        {   //Add an eventTrigger component to the slot's GameObject
            GameObject buttonObject = slot.gameObject.GetComponentInChildren<Button>().gameObject;
            buttonObject.AddComponent<EventTrigger>();

            //Create a trigger for mouse entry
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { OnEnter(slot.value); });

            //Add the entry to the button
            buttonObject.GetComponent<EventTrigger>().triggers.Add(entry);

            //Create a trigger for mouse exit
            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener((eventData) => { OnExit(); });

            //Add the entry to the button
            buttonObject.GetComponent<EventTrigger>().triggers.Add(exit);
        }
    }

    public void OnEnter(int value)
    {
        priceLabel.text = "$" + value;
    }

    public void OnExit()
    {
        priceLabel.text = "$_";
    }
}
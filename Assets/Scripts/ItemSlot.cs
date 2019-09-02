using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    private Item item;

    public ItemSlot() {
        item = null;
    }

    public bool isEmpty() {
        return item == null;
    }

    public Item getItem() {
        return item;
    }

    public void setItem(Item item) {
        this.item = item;
    }
}

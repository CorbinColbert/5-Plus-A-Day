using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PriceUpdater : MonoBehaviour
{
    public void OnEnter()
    {
        int cost = GetComponentInParent<ShopSlot>().value;
        GameObject.Find("ShopCost").GetComponent<Text>().text = "$" + cost;
    }

    public void OnExit()
    {
        GameObject.Find("ShopCost").GetComponent<Text>().text = "";
    }
}
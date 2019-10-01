using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PriceUpdater : MonoBehaviour
{
    public void OnEnter()
    {
        print("OnMouseEnter");
        int cost = GetComponentInParent<Slot>().cost;
        GameObject.Find("ShopCost").GetComponent<Text>().text = "$" + cost;
    }

    public void OnExit()
    {
        print("OnMouseExit");
        GameObject.Find("ShopCost").GetComponent<Text>().text = "";
    }
}
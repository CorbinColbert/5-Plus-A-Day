using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TroopPlacer : MonoBehaviour
{   
    //Prefabs
    public GameObject prefabCarrot;
    public GameObject prefabPear;
    public GameObject prefabTangerine;
    public GameObject prefabOrange;
    public GameObject prefabBanana;
    public GameObject prefabGrape;
    public GameObject prefabCherry;
    public GameObject prefabLemon;
    public GameObject prefabPeach;
    public GameObject prefabRadish;
    public GameObject prefabPepper;
    public GameObject prefabCake;

    public Inventory inventory; // Connected inventory
    public BattleManager manager; // TroopManager

    // Update is called once per frame
    void Update()
    {
        if (!inventory.gameObject.activeSelf) {
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && inventory.selectedSlot != null)
        {
            if (true) // TODO if player has enough of the selected unit
            Place();
        }
    }

    private void Place()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit))
        {
            // The ray did not hit anything
            return;
        }

        // The object which the ray hit
        GameObject hitObject = hit.transform.gameObject;

        // If the object that was clicked on isn't a placeable tile
        if (!hitObject.TryGetComponent<TroopNode>(out TroopNode node))
        {
            return;
        }

        // Get the prefab that corresponds to the troop type
        TroopType type = inventory.selectedSlot.troopType;
        GameObject troop = GetRelativePrefab(type);

        // Check that the troop type matches expected type
        if (troop.GetComponent<Troop>().troopType != type)
        {
            throw new FormatException("Mismatch detected in Troop Placer, unexpected troop type");
        }

        //Configure the troop
        troop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Place it on the scene
        Transform nT = node.transform;
        Vector3 placementPosition = new Vector3(nT.position.x, nT.position.y + 0.65f , nT.position.z);
        GameObject instantiatedTroop = Instantiate(troop, placementPosition, Quaternion.identity);
        manager.Deploy(troop);
        instantiatedTroop.SetActive(true);
    }

    //Method to get the prefab depending on the troopType
    private GameObject GetRelativePrefab(TroopType troopType)
    {
        switch (troopType)
        {
            case TroopType.CARROT:
                return prefabCarrot ;
            case TroopType.PEAR:
                return prefabPear ;
            case TroopType.TANGERINE:
                return prefabTangerine ;
            case TroopType.ORANGE:
                return prefabOrange ;
            case TroopType.BANANA:
                return prefabBanana ;
            case TroopType.GRAPE:
                return prefabGrape ;
            case TroopType.CHERRY:
                return prefabCherry ;
            case TroopType.LEMON:
                return prefabLemon ;
            case TroopType.PEACH:
                return prefabPeach ;
            case TroopType.RADISH:
                return prefabRadish ;
            case TroopType.PEPPER:
                return prefabPepper ;
            case TroopType.CAKE:
                return prefabCake ;
            default:
                return null;
        }
    }
}

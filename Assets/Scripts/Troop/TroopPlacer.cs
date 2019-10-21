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
        if (Input.GetMouseButtonDown(0) && inventory.selectedSlot != null)
        {
            Place();
        }
    }

    private void Place()
    {
        // Cast a ray with maximum distance 100
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 100.0f);

        if (hit.transform == null)
        {
            // The ray did not hit anything
            return;
        }

        // The object which the ray hit
        GameObject hitObject = hit.transform.gameObject;

        // Get the prefab that corresponds to the troop type
        TroopType type = inventory.selectedSlot.troopType;
        GameObject troop = GetRelativePrefab(type);

        // Check that the troop type matches expected type
        if (troop.GetComponent<Troop>().troopType != type)
        {
            throw new FormatException("Mismatch detected in Troop Placer, unexpected troop type");
        }

        // Place it on the scene
        Vector3 placementPosition = new Vector3(0, (hitObject.transform.position.y * 0.5f) + (troop.transform.localScale.y * 0.5f), 0);
        GameObject instantiatedTroop = Instantiate(troop, placementPosition, Quaternion.identity);
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

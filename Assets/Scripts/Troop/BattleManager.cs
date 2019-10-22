using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * BattleManager Class - Singleton
 ************************************
 * Used to keep track of living and dead enemy troops
 */
public class BattleManager : MonoBehaviour
{
    private List<GameObject> livingPlayerTroops;
    private List<GameObject> deadPlayerTroops;
    private List<GameObject> livingEnemyTroops;
    private List<GameObject> deadEnemyTroops;

    private void Start()
    {
        livingPlayerTroops = new List<GameObject>();
        deadPlayerTroops = new List<GameObject>();
        livingEnemyTroops = new List<GameObject>();
        deadEnemyTroops = new List<GameObject>();

        print("BattleManager Start()");

        ScanSceneForEnemies();
    }

    private void ScanSceneForEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyTroop"); 
        foreach (GameObject obj in enemies)
        {
            print("Found enemy in scene");
            livingEnemyTroops.Add(obj);
        }
    }

    // Caller must provide which side they are on
    public List<GameObject> GetOpponents(bool isPlayerTroop)
    {
        if (isPlayerTroop)
        {
            return livingEnemyTroops;
        }
        else
        {
            return livingPlayerTroops;
        }
    }

    // Deploy a GameObject with Troop script already attatched (a prefab)
    public void Deploy(GameObject troop)
    {
        bool isPlayerTroop = troop.CompareTag("PlayerTroop");

        if (isPlayerTroop)
        {
            livingPlayerTroops.Add(troop);
        }
        else
        {
            livingEnemyTroops.Add(troop);
        }

        //Subscribe this class's UpdateTroops method to that troop's death event
        troop.GetComponent<Troop>().OnDeathEvent += UpdateTroops;
    }

    // Moves the dead troops to the graveyard so that other troops stop attacking
    private void UpdateTroops()
    {
        foreach (GameObject g in livingPlayerTroops)
        {
            if (g.GetComponent<Troop>().alive == false)
            {
                livingPlayerTroops.Remove(g);
                deadPlayerTroops.Add(g);
            }
        }

        foreach (GameObject g in livingEnemyTroops)
        {
            if (g.GetComponent<Troop>().alive == false)
            {
                livingEnemyTroops.Remove(g);
                deadEnemyTroops.Add(g);
            }
        }
    }
}

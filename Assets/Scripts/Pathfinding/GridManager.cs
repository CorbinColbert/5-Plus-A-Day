using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //make sure troops dont go over their maximum
    //keep track of troops
    //
    GameObject[] playerTroops;
    GameObject[] enemyTroops;

    private void Start()
    {
        //max amount of troops allowed on the grid of each type
        playerTroops = new GameObject[24]; 
        enemyTroops = new GameObject[24];
    }

    public void Update()
    {
        //update the lists of units
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is here to keep track of the different troops on the grid
public class GridManager : MonoBehaviour
{
    //TODO: make sure troops dont go over their maximum
    //TODO: keep track of troops
    //
    GameObject[] playerTroops;
    GameObject[] enemyTroops;

    private void Start()
    {
        //TODO: max amount of troops allowed on the grid of each type
        playerTroops = new GameObject[24]; 
        enemyTroops = new GameObject[24];
    }

    public void Update()
    {
        //TODO: update the lists of units
    }
}

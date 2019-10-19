using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Will store the Player Data from the current active game

    //public int level; //Used when we will change the levels later on
    public int health;
    public float[] positions; // Variable for the positions of troops

    public PlayerData(PlayerData player)//Needs to take in variables from the script that the player uses
    {
        //Setting all the player data from the running script into variables
        //for example --> health = player.health; 
    }
}

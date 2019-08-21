using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board2 : MonoBehaviour
{
    public GameObject[,] TileArray;
    public int ArraySizeX, ArraySizeY;

    void Start()
    {
        TileArray = new GameObject[ArraySizeX,ArraySizeY];
    }
}

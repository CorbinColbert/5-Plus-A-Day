using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[,] tile;
    public int xMax, yMax; //0 Based
    public float unitHoverDistance;

    public bool IsInbounds(int x, int y) {
        bool xInbounds = (x >= 0 && x <= xMax);
        bool yInbounds = (y >= 0 && y <= yMax);

        return xInbounds && yInbounds;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Loyalty loyalty;
    public Stats stats;
    private Tile tile;
    private Unit target;

    void Start() {
        stats = new Stats(100, 100);
    }

    //Called once per frame
    void Update() {

    }

    public Tile GetTile() {
        return tile;
    }

    public void placeOnBoard(Tile tile) {
        this.tile = tile;
        tile.SetUnit(this);
    }
}

public enum Loyalty {
    ALLY,
    ENEMY
}

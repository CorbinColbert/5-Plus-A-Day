using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Board board;
    private int xPos, yPos; //Position on the grid, 0 based
    private Unit unit;
    private Vector3 unitPos;

    // Start is called before the first frame update
    void Start()
    {
        print("I am a tile!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile[] GetAffectedTiles(SpecialAttack sp) {
        List<Pair> relative = sp.getAOE();

        Tile[] involved = new Tile[relative.Count];
        int i = 0;
        foreach (Pair pair in relative) {
            Pair absolute = pair.Add(this.xPos, this.yPos);
            if (board.IsInbounds(absolute.x, absolute.y))
                involved[i++] = board.tile[absolute.x, absolute.y];
        }

        return involved;
    }

    public bool IsNeighbour(int xOther, int yOther) {
        int absDeltaX = Mathf.Abs(xOther - xPos);
        int absDeltaY = Mathf.Abs(yOther - yPos);

        return (absDeltaX <= 1) && (absDeltaY <= 1);
    }

    public Tile GetNeighbour(Direction direction) {
        switch (direction) {
            case Direction.NORTH:
                if (board.IsInbounds(xPos, yPos + 1)) {
                    return board.tile[xPos, yPos + 1];
                }
            break;
            case Direction.EAST:
                if (board.IsInbounds(xPos + 1, yPos)) {
                    return board.tile[xPos + 1, yPos];
                }
            break;
            case Direction.SOUTH:
                if (board.IsInbounds(xPos, yPos - 1)) {
                    return board.tile[xPos, yPos - 1];
                }
            break;
            case Direction.WEST:
                if (board.IsInbounds(xPos - 1, yPos)) {
                    return board.tile[xPos - 1, yPos];
                }
            break;
        }
        return null;
    }

    public bool HasUnit() {
        return this.unit != null;
    }

    public bool SetUnit(Unit unit) {
        if (this.unit == null) {
            return false;
        } else {
            this.unit = unit;
            return true;
        }
    }

    public Unit GetUnit() {
        return unit;
    }
}

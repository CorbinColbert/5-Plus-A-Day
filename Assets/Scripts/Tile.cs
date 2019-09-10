using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Board board;
    public Unit unit;
    public int xPos; //Position on the grid, 0 based
    public int zPos; //Position on the grid, 0 based

    // Start is called before the first frame update
    void Start()
    {
        print("Tile Initialised");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasUnit() {
        return unit != null;
    }

    public Tile[] GetAffectedTiles(SpecialAttack sp) {
        List<Pair> relative = sp.getAOE();

        Tile[] involved = new Tile[relative.Count];
        int i = 0;
        foreach (Pair pair in relative) {
            Pair absolute = pair.Add(this.xPos, this.zPos);
            if (board.IsInbounds(absolute.x, absolute.y))
                involved[i++] = board.tile[absolute.x, absolute.y];
        }

        return involved;
    }

    public bool IsNeighbour(int xOther, int yOther) {
        int absDeltaX = Mathf.Abs(xOther - xPos);
        int absDeltaY = Mathf.Abs(yOther - zPos);
 
        print("Call to IsNeighbour: "+((absDeltaX <= 1) && (absDeltaY <= 1)));
    
        return (absDeltaX <= 1) && (absDeltaY <= 1);
    }

    public Tile GetNeighbour(Direction direction) {
        switch (direction) {
            case Direction.NORTH:
                if (board.IsInbounds(xPos, zPos + 1)) {
                    return board.tile[xPos, zPos + 1];
                }
            break;
            case Direction.EAST:
                if (board.IsInbounds(xPos + 1, zPos)) {
                    return board.tile[xPos + 1, zPos];
                }
            break;
            case Direction.SOUTH:
                if (board.IsInbounds(xPos, zPos - 1)) {
                    return board.tile[xPos, zPos - 1];
                }
            break;
            case Direction.WEST:
                if (board.IsInbounds(xPos - 1, zPos)) {
                    return board.tile[xPos - 1, zPos];
                }
            break;
        }
        return null;
    }
}

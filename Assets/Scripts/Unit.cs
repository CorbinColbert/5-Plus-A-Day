using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Board board;
    public Tile tile;
    public int xPos;
    public int zPos;

    public Loyalty loyalty;
    public Stats stats;
    public ItemSlot itemSlot;
    public Unit target;

    void Start() {
        stats = new Stats(100, 100);
    }

    void Update() {
        if (HasTarget()) {
            if (TargetIsInRange()) {
                AttackTarget();
            } else {
                MoveTowardsTarget();
            }
        } else {
            FindTarget();
        }  
    }

    public bool HasTarget() {
        return (target != null);
    }

    //Dumb finding algorithm for testing only
    public void FindTarget() {
        for (int x = 0; x < board.xMax; x++) {
            for (int z = 0; z < board.zMax; z++) {
                if (board.tile[x, z].HasUnit()) {
                    if (board.tile[x, z].unit.loyalty != this.loyalty) {
                        target = board.tile[x, z].unit;
                    }
                }
            }
        }
    }

    public void MoveTowardsTarget() {
        float currentDistance = Board.PyDistance(xPos, target.xPos, zPos, target.zPos);
        float testDistance;

        if (board.IsInbounds(xPos+1, zPos)) {
            testDistance = Board.PyDistance(xPos+1, target.xPos, zPos, target.zPos);

            if (testDistance < currentDistance) {
                return;
            }

        }
        if (board.IsInbounds(xPos-1, zPos)) {
            testDistance = Board.PyDistance(xPos-1, target.xPos, zPos, target.zPos);
        }
        if (board.IsInbounds(xPos, zPos+1)) {
            testDistance = Board.PyDistance(xPos, target.xPos, zPos+1, target.zPos);
        }
        if (board.IsInbounds(xPos, zPos-1)) {
            testDistance = Board.PyDistance(xPos, target.xPos, zPos-1, target.zPos);
        }
    }

    public bool TargetIsInRange() {
        if (HasTarget()) {
            if (board.IsInbounds(xPos, zPos)) {
                print("xPos: "+xPos+" zPos: "+zPos);
                return board.tile[xPos, zPos].IsNeighbour(target.xPos, target.zPos);
            }
        }
        return false;
    }

    public void AttackTarget() {
        print("I, "+gameObject.name +", are attacking the unit known as "+target.name);
    }

    public Item getItem() {
        return itemSlot.getItem();
    }

}

public enum Loyalty {
    ALLY,
    ENEMY
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Stats stats;
    protected Pair position;
    protected Board board;
    protected ItemSlot itemSlot;
    public Unit target;

    void Start() {
        stats = new Stats(100, 100);
    }

    //Called once per frame
    void Update() {

    }

    public Pair getBoardPosition() {
        return this.position;
    }

    public Item getItem() {
        return itemSlot.getItem();
    }

    public void placeOnBoard(Board board, Pair position) {
        this.board = board;
        this.position = position;
    }
}

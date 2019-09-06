using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair {
    public int x;
    public int y;

    public Pair(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public Pair Add(int x, int y) {
        return new Pair(this.x + x, this.y + y);
    }
}

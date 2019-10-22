using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class represents nodes that will be used to form a grid
public class Node : ItemHeapIndex<Node>
{
    public bool viableNode;
    public bool unitOnTop;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int hCost;
    public int gCost;
    public Node parent;
    int heapIndex;

    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }
    public Node(bool viableNode, bool unitOnTop, Vector3 worldPosition, int gridX, int gridY)
    {
        this.viableNode = viableNode;
        this.unitOnTop = unitOnTop;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class manages individual units pathfinding and placement
public class UnitPathing : MonoBehaviour
{
    public List<Node> path;
    public GameObject grid;
    private Node nodeUnitOnTopOf;
    private Node tempNode;
    int movementPoints;

    private void Update()
    {
        if (nodeUnitOnTopOf == null)
        {
            nodeUnitOnTopOf = grid.GetComponent<NodeGrid>().getNodeFromWorld(gameObject.transform.position);
            tempNode = nodeUnitOnTopOf;
        }
        else
        {
            tempNode = nodeUnitOnTopOf;
            nodeUnitOnTopOf = grid.GetComponent<NodeGrid>().getNodeFromWorld(gameObject.transform.position);
        }

        if (nodeUnitOnTopOf == tempNode)
        {
            tempNode.unitOnTop = false;
        }
        else
        {
            tempNode.unitOnTop = true;
            tempNode = nodeUnitOnTopOf;
            tempNode.unitOnTop = false;
        }
    }

    private void snapToGrid()
    {
        //TODO: ?   

        //GRID
        // [-3.5, +3.5] [-2.5, +3.5] [-1.5, +3.5] [-0.5, +3.5] [+0.5, +3.5] [+1.5, +3.5] [+2.5, +3.5] [+3.5, +3.5]
        // [-3.5, +2.5] [-2.5, +2.5] [-1.5, +2.5] [-0.5, +2.5] [+0.5, +2.5] [+1.5, +2.5] [+2.5, +2.5] [+3.5, +2.5]
        // [-3.5, +1.5] [-2.5, +1.5] [-1.5, +1.5] [-0.5, +1.5] [+0.5, +1.5] [+1.5, +1.5] [+2.5, +1.5] [+3.5, +1.5]
        // [-3.5, +0.5] [-2.5, +0.5] [-1.5, +0.5] [-0.5, +0.5] [+0.5, +0.5] [+1.5, +0.5] [+2.5, +0.5] [+3.5, +0.5]
        // [-3.5, -0.5] [-2.5, -0.5] [-1.5, -0.5] [-0.5, -0.5] [+0.5, -0.5] [+1.5, -0.5] [+2.5, -0.5] [+3.5, -0.5]
        // [-3.5, -1.5] [-2.5, -1.5] [-1.5, -1.5] [-0.5, -1.5] [+0.5, -1.5] [+1.5, -1.5] [+2.5, -1.5] [+3.5, -1.5]
        // [-3.5, -2.5] [-2.5, -2.5] [-1.5, -2.5] [-0.5, -2.5] [+0.5, -2.5] [+1.5, -2.5] [+2.5, -2.5] [+3.5, -2.5]
        // [-3.5, -3.5] [-2.5, -3.5] [-1.5, -3.5] [-0.5, -3.5] [+0.5, -3.5] [+1.5, -3.5] [+2.5, -3.5] [+3.5, -3.5]

        // Mathf.Round() + 0.5;  used to snap to our grid
        //cant leave the grid
    }

    private void getPathing(GameObject target)
    {
        path = grid.GetComponent<Pathfinding>().FindPath(gameObject, target);
    }

    void getClosestTargrt()
    {
        //TODO: 
    }

    private void moveAlongPath()
    {
        //TODO: 
    }
}

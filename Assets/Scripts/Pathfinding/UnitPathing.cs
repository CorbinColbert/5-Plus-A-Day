using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathing : MonoBehaviour
{
    public List<Node> path;
    public GameObject grid;
    private Node nodeUnitOnTopOf;
    private Node tempNode;

    public void Start()
    {
        //nodeUnitOnTopOf = grid.GetComponent<NodeGrid>().getNodeFromWorld(gameObject.transform.position);
    }

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
    }

    private void getPathing(GameObject target)
    {
        path = grid.GetComponent<Pathfinding>().FindPath(gameObject, target);
    }

    void getClosestTargrt()
    {

    }

    private void moveAlongPath()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathing : MonoBehaviour
{
    public List<Node> path;
    public GameObject grid;
    private Node nodeUnitOnTopOf;

    public void OnEnable()
    {
        nodeUnitOnTopOf = grid.GetComponent<NodeGrid>().getNodeFromWorld(gameObject.transform.position);
    }

    private void snapToGrid()
    {

    }
    
    private void moveAlongPath()
    {
        foreach (Node node in path)
        {
            
        }
    }
}

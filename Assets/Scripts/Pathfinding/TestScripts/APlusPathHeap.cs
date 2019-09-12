using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is for the creation af a path from one location to another
//this class is a modified version of Sebastian Lague pathfinding class from his A* pathfinding series on youtube and permision to modify is given under the MIT licence
public class APlusPathHeap : MonoBehaviour
{
    public Transform pathingUnit, target;

    NodeGrid grid;

    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
    }

    private void Update()
    {
        FindPath(pathingUnit.position, target.position);
    }
    void FindPath(Vector3 start, Vector3 target)
    {
        Node startNode = grid.getNodeFromWorld(start);
        Node targetNode = grid.getNodeFromWorld(target);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                TraceNodePath(startNode, targetNode);
                return;
            }

            foreach (Node ajacentNode in grid.getAjacentNodes(currentNode))
            {
                if (!ajacentNode.viableNode || !ajacentNode.unitOnTop ||closedSet.Contains(ajacentNode))
                {
                    if (ajacentNode != startNode && ajacentNode != targetNode)
                    {
                        continue;
                    }
                }

                int newMovementCostToAjacentNode = currentNode.gCost + getDistance(currentNode, ajacentNode);
                if (newMovementCostToAjacentNode < ajacentNode.gCost || !openSet.Contains(ajacentNode))
                {
                    ajacentNode.gCost = newMovementCostToAjacentNode;
                    ajacentNode.hCost = getDistance(ajacentNode, targetNode);
                    ajacentNode.parent = currentNode;

                    if (!openSet.Contains(ajacentNode))
                    {
                        openSet.Add(ajacentNode);
                    }
                }
            }
        }
    }

    void TraceNodePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        path.Remove(endNode);

        //pathingUnit.gameObject.GetComponent<UnitPathing>().path = path;

        grid.path = path;
        pathingUnit.gameObject.GetComponent<UnitPathing>().path = path;
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}

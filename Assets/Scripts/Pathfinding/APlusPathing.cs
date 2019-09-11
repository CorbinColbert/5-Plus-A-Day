using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlusPathing : MonoBehaviour
{
    public Transform seeker, target;

    NodeGrid grid;

    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 start, Vector3 target)
    {
        Node startNode = grid.getNodeFromWorld(start);
        Node targetNode = grid.getNodeFromWorld(target);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost ||
                    openSet[i].fCost == currentNode.fCost &&
                    openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                TraceNodePath(startNode, targetNode);
                return;
            }

            foreach (Node ajacentNode in grid.getAjacentNodes(currentNode))
            {
                if (!ajacentNode.walkable || closedSet.Contains(ajacentNode))
                {
                    continue;
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

        grid.path = path;
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

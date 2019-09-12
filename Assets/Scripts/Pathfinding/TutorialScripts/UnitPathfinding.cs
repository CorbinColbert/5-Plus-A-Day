using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathfinding : MonoBehaviour
{
    //public Transform pathingUnit, target;

    PathManager pathManager;
    NodeGrid grid;

    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
        pathManager = GetComponent<PathManager>();
    }
    public void TryFindPath(GameObject pathingUnit, GameObject targetUnit)
    {
        StartCoroutine(FindPath(pathingUnit, targetUnit));
    }
    IEnumerator FindPath(GameObject pathingUnit, GameObject targetUnit)
    {
        Vector3 start = pathingUnit.transform.position;
        Vector3 target = targetUnit.transform.position;
        Node startNode = grid.getNodeFromWorld(start);
        Node targetNode = grid.getNodeFromWorld(target);
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        bool successful = false;
        List<Node> path = null;

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                TraceNodePath(startNode, targetNode);
                successful = true;
            }

            foreach (Node ajacentNode in grid.getAjacentNodes(currentNode))
            {
                if (!ajacentNode.viableNode || !ajacentNode.unitOnTop || closedSet.Contains(ajacentNode))
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
        yield return null;
        if (successful)
        {
            path = TraceNodePath(startNode, targetNode);
        }
        pathManager.pathRequestDone(path, successful);
    }

    List<Node> TraceNodePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        path.Remove(endNode); // so we dont get the node directly below the target

        grid.path = path;
        return path;
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
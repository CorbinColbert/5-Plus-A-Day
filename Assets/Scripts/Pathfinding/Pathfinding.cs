using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is for the creation af a path from one location to another
//this class is a modified version of Sebastian Lague pathfinding class from his A* pathfinding series on youtube and permision to modify is given under the MIT licence
public class Pathfinding : MonoBehaviour
{
    NodeGrid grid;
    PathHelper pathhelper; 

    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
        pathhelper = GetComponent<PathHelper>();
    }

    public void PathRequest(PathRequest request)
    {
        StartCoroutine(FindPath(request.pathingUnit, request.targetUnit));
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
        bool isSucessful = false;
        List<Node> path = null;

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                isSucessful = true;
                break;
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
                    else
                    {
                        openSet.UpdateItem(ajacentNode);
                    }
                }
            }
        }

        yield return null;

        if (isSucessful)
        {
            path = TraceNodePath(startNode, targetNode);
        }

        pathhelper.PathRequestFinished(path, isSucessful);
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
        path.Remove(endNode); // so we dont get the node directly under the target
        endNode.parent.unitOnTop = false;
        return path;
    }

    public static int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

    public static Vector3[] PathToVectors(List<Node> path)
    {
        int pathSize = path.Count;
        Vector3 tempVector;
        Vector3[] vectorPath = new Vector3[pathSize];
        int index = 0;

        foreach (Node node in path)
        {
            tempVector = node.worldPosition;
            vectorPath.SetValue(tempVector, index);
            index++;
        }

        return vectorPath;
    }
}

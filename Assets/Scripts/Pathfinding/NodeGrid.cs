using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{ 
    public bool displayPathOnly;
    public LayerMask viabilityMask;
    public LayerMask occupiedMask;
    public Vector2 gridSize;
    public float nodeRadius;
    Node[,] grid;
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    public List<Node> path;
    public int MaxSize{ get{return gridSizeX * gridSizeY;} }
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 gridBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 nodePlacementPoint = gridBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool isNodeViable = !(Physics.CheckSphere(nodePlacementPoint,nodeRadius, viabilityMask)); //refactor to include occupiedMask to see if a troop is standing on this node
                //if (!(Physics.CheckSphere(nodePlacementPoint, nodeRadius, viabilityMask)) || !(Physics.CheckSphere(nodePlacementPoint, nodeRadius, occupiedMask)))
                //{
                //    isNodeViable = true;
                //}
                //else
                //{
                //    isNodeViable = false;
                //}
                grid[x, y] = new Node(isNodeViable, nodePlacementPoint, x, y);
            }
        }
    }
    public Node getNodeFromWorld(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridSize.x/2) / gridSize.x;
        float percentY = (worldPosition.z + gridSize.y/2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
        return grid[x,y];
    }
    public List<Node> getAjacentNodes(Node node)
    {
        List<Node> ajacentNodes = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int inBoundsX = node.gridX + x;
                int inBoundsY = node.gridY + y;

                if (inBoundsX >= 0 && inBoundsX < gridSizeX && inBoundsY >= 0 && inBoundsY < gridSizeY)
                {
                    ajacentNodes.Add(grid[inBoundsX, inBoundsY]);
                }
            }
        }
        return ajacentNodes;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x,1,gridSize.y));
        if (displayPathOnly)
        {
            if (path != null)
            {
                foreach (Node node in path)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
        else
        {
            if (grid != null)
            {
                foreach (Node node in grid)
                {
                    Gizmos.color = (node.viableNode) ? Color.white : Color.red;
                    if (path != null)
                    {
                        if (path.Contains(node))
                        {
                            Gizmos.color = Color.yellow;
                        }
                    }
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
                    //Gizmos.DrawSphere(node.worldPosition, 0.05f);
                }
            }
        }
    }

    
}

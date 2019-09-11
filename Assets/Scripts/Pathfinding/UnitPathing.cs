using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathing : MonoBehaviour
{
    public List<Node> path;

    public void moveAlongPath()
    {
        foreach (Node node in path)
        {
            gameObject.transform.position = Vector3.Lerp(node.worldPosition, node.parent.worldPosition, 10);
        }
    }
}

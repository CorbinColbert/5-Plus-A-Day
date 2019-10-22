using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathing : MonoBehaviour
{
    public Node[] path;
    public GameObject grid;
    public Node nodeUnitOnTopOf;
    public bool hasPathToFollow = false;
    public float moveSpeed;

    private float timer = 0;
    private Vector3 tempStartPos;
    private int index = 0;
    private Node tempNode = null;

    private void FixedUpdate()
    {
        UnitNodeUpdate();

        if (hasPathToFollow)
        {
            //follow the path
            transform.position = Vector3.Lerp(tempStartPos, path[index].worldPosition, timer);

            if (transform.position == path[index].worldPosition)
            {
                index++;
                timer = 0;
                tempStartPos = transform.position;
            }

            if (TryGetComponent<Troop>(out Troop troop)) {
                timer += Time.deltaTime * GetComponent<Troop>().moveSpeed;
            } else {
                timer += Time.deltaTime * 1;
            }
            
            //at end, turn hasPathToFollow to false and index and timer back to 0
            if (transform.position == path[path.Length - 1].worldPosition)
            {
                hasPathToFollow = false;
                timer = 0;
                index = 0;
                path = null;
            }
        }
    }

    private void UnitNodeUpdate()
    {
        if (grid.TryGetComponent<NodeGrid>(out NodeGrid nodeGrid))
        {
            if (nodeUnitOnTopOf == null)
            {
                nodeUnitOnTopOf = nodeGrid.getNodeFromWorld(gameObject.transform.position);
                tempNode = nodeUnitOnTopOf;
            }
            else
            {
                Node compareNode = nodeUnitOnTopOf = nodeGrid.getNodeFromWorld(gameObject.transform.position);
                if (nodeUnitOnTopOf != compareNode)
                {
                    tempNode = nodeUnitOnTopOf;
                    nodeUnitOnTopOf = compareNode;
                }
            }

            if (nodeUnitOnTopOf == tempNode)
            {
                tempNode.unitOnTop = true;
            }
            else
            {
                tempNode.unitOnTop = false;
                tempNode = nodeUnitOnTopOf;
                tempNode.unitOnTop = true;
            }
        }
    }

    private void Start()
    {
        tempStartPos = transform.position;
        grid = GameObject.Find("GameManager");
    }

    //call this to get a path
    public void GetPathing(GameObject target)
    {
        if (!hasPathToFollow)
        {
            grid.GetComponent<PathHelper>().RequestAPath(target, gameObject, doStuffWithPath);
        }
        else
        {
            throw new System.InvalidOperationException("There is alredy a path being processed!!");
        }
    }

    //callback method from getPathing
    public void doStuffWithPath(List<Node> path, bool wasSuccessfull)
    {
        if (wasSuccessfull)
        {
            this.path = path.ToArray();
            hasPathToFollow = true;
        }
        else
        {
            //TODO: pathing has failed. needs code to handle this.
        }
    }
}

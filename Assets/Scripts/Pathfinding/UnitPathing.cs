using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class manages individual units pathfinding and placement
public class UnitPathing : MonoBehaviour
{
    public Node[] path;
    public GameObject grid;
    private Node nodeUnitOnTopOf;
    public GameObject target;
    bool hasPathToFollow = false;
    float timer = 0;
    public float moveSpeed;
    Vector3 tempStartPos;
    int index = 1;
    public KeyCode key;

    private void FixedUpdate()
    {
        Node tempNode;

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

        if (Input.GetKeyDown(key))
        {
            GetPathing(target);
        }

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

            timer += Time.deltaTime * moveSpeed;

            //at end turn hasPathToFollow to false and index and timer back to 0
            if (transform.position == path[path.Length - 1].worldPosition)
            {
                hasPathToFollow = false;
                timer = 0;
                index = 0;
                path = null;
            }
        }
    }



    private void Start()
    {
        //get position at start of object
        tempStartPos = transform.position;
    }

    
    public void GetPathing(GameObject target)
    {
        if (!hasPathToFollow)
        {
            grid.GetComponent<PathHelper>().RequestAPath(target, gameObject, doStuffWithPath);
            //PathHelper.RequestAPath(target, gameObject, doStuffWithPath);
        }
        // wait for current path to finish
    }

    public void doStuffWithPath(List<Node> path , bool wasSuccessfull)
    {
        if (wasSuccessfull)
        {
            print("the path request returned successfull");
            this.path = path.ToArray();
            hasPathToFollow = true;
        }
        else
        {
            print("the path request did not return successfull");
            //pathing failed
        }
    }
}

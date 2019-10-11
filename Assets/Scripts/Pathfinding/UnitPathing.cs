using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathing : MonoBehaviour
{
    public Node[] path;
    public GameObject grid;
    //public GameObject target;
    public bool hasPathToFollow = false;
    float timer = 0;
    public float moveSpeed;
    Vector3 tempStartPos;
    int index = 0;

    private void FixedUpdate()
    {

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



    private void Start()
    {
        //get position at start of object
        tempStartPos = transform.position;
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
            //pathing failed
            //TODO: stuff here
        }
    }
}

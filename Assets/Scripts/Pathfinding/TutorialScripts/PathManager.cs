using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This class is here to manage the distribution of paths to multiple troops
public class PathManager : MonoBehaviour
{
    //TODO: main aim is to give paths out 1 bye 1 to troops so that there are no overlaps in placement
    //TODO: troops ask the path manager for a path
    //TODO: implement a queue for troops asking
    //TODO: 

    Queue<Request> requestsQueue = new Queue<Request>();
    Request tempRequest;
    static PathManager pathManagerInstance;
    UnitPathfinding pathfindingScript;
    bool currentlyRequestingPath;

    private void Awake()
    {
        pathManagerInstance = this;
        pathfindingScript = GetComponent<UnitPathfinding>();
    }

    public static void pathRequest(GameObject pathingUnit, GameObject targetUnit, Action<List<Node>, bool> response)
    {
        Request request = new Request(pathingUnit, targetUnit, response);
        pathManagerInstance.requestsQueue.Enqueue(request);
        pathManagerInstance.tryRequestPath();
    }

    void tryRequestPath()
    {
        if (currentlyRequestingPath && requestsQueue.Count > 0)
        {
            tempRequest = requestsQueue.Dequeue();
            currentlyRequestingPath = true;
            pathfindingScript.TryFindPath(tempRequest.pathingUnit, tempRequest.targetUnit);
        }
    }

    public void pathRequestDone(List<Node> path, bool successful)
    {
        tempRequest.response(path, successful);
        currentlyRequestingPath = false;
        tryRequestPath();
    }

    struct Request
    {
        public GameObject pathingUnit;
        public GameObject targetUnit;
        public Action<List<Node>, bool> response;

        public Request(GameObject pathingUnit, GameObject targetUnit, Action<List<Node>, bool> response)
        {
            this.pathingUnit = pathingUnit;
            this.targetUnit = targetUnit;
            this.response = response;
        }
    }
}

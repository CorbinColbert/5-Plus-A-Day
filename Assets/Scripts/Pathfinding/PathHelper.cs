using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathHelper : MonoBehaviour
{
    Queue<PathRequest> pathRequests = new Queue<PathRequest>();
    PathRequest tempPathRequest;
    bool requesting = false;
    //PathHelper pathHelper;
    Pathfinding pathfinder;

    private void Awake()
    {
        pathfinder = GetComponent<Pathfinding>();
    }
    public void RequestAPath(GameObject targetTroop, GameObject pathRequester, Action<List<Node>, bool> requestResponce)
    {
        PathRequest request = new PathRequest(targetTroop, pathRequester, requestResponce);
        pathRequests.Enqueue(request);
        nextInQueue();
    }

    public void PathRequestFinished(List<Node> path, bool sucessful)
    {
        tempPathRequest.response(path, sucessful);
    }

    void nextInQueue()
    {
        if (!requesting && pathRequests.Count > 0)
        {
            //process next path request
            tempPathRequest = pathRequests.Dequeue();
        }
    }
}


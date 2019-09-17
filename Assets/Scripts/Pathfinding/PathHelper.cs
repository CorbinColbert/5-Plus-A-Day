using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathHelper : MonoBehaviour
{
    Queue<PathRequest> pathRequests = new Queue<PathRequest>();
    PathRequest tempPathRequest;
    bool requesting = false;
    PathHelper pathHelper;
    Pathfinding pathfinder;

    private void Awake()
    {
        pathHelper = this;
        pathfinder = GetComponent<Pathfinding>();
    }
    public void RequestAPath(GameObject targetTroop, GameObject pathRequester, Action<List<Node>, bool> requestResponce)
    {
        PathRequest request = new PathRequest(targetTroop, pathRequester, requestResponce);
    }

    public void PathRequestFinished(List<Node> path, bool sucessful)
    {

    }

    void nextInQueue()
    {
        if (!requesting && pathRequests.Count > 0)
        {
            //process next path request
        }
    }
}


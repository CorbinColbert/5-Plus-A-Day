using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct PathRequest
{
    public GameObject pathingUnit;
    public GameObject targetUnit;
    public Action<List<Node>, bool> response;

    public PathRequest(GameObject targetUnit, GameObject pathingUnit, Action<List<Node>, bool> response)
    {
        this.targetUnit = targetUnit;
        this.pathingUnit = pathingUnit;
        this.response = response;
    }
}
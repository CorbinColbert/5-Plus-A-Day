using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tile;
    public GameObject[,] tileArray;
    public int xSize, zSize;
    public float unitHoverDistance;

    public void Start() {
        unitHoverDistance = 1.0f;

        tileArray = new GameObject[xSize, zSize];
        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++) {
                tileArray[x, z] = Instantiate(tile, new Vector3(transform.position.x,transform.position.y, transform.position.z) , Quaternion.identity, gameObject.transform);
            }
        }
    }

    public bool IsInbounds(int x, int z) {
        bool xInbounds = (x > 0 && x < xSize);
        bool yInbounds = (z > 0 && z < zSize);

        return xInbounds && yInbounds;
    }
}
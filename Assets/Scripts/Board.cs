using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    
    public int xMax;    //To be used in the editor
    public int zMax;    //To be used in the editor
    
    public Tile[,] tile;

    public void Start() {
        tile = new Tile[xMax + 1, zMax+1];
        for (int z = 0; z <= zMax; z++) {
            for (int x = 0; x <= xMax; x++) {
                float xTrans = (transform.position.x + x) * 1.1f;
                float yTrans = transform.position.y;
                float zTrans = (transform.position.z + z) * 1.1f;
                GameObject g = Instantiate(tilePrefab, new Vector3(xTrans, yTrans, zTrans), Quaternion.identity, gameObject.transform);

                g.name = "Tile "+x+", "+z;

                g.AddComponent<Tile>();
                tile[x, z] = g.GetComponent<Tile>();
                tile[x, z].xPos = x;
                tile[x, z].zPos = z;
                tile[x, z].board = this;
            }
        }
        print("Board Initialised with xMax: "+xMax+" zMax: "+zMax);
    }

    public void Update() {

    }

    public bool IsInbounds(int x, int z) {
        bool xInbounds = (x >= 0 && x <= xMax);
        bool yInbounds = (z >= 0 && z <= zMax);

        return xInbounds && yInbounds;
    }

    public static float PyDistance(int x1, int x2, int y1,int y2) {
        return Mathf.Sqrt((x1-x2) * (x1-x2) + (y1-y2) * (y1-y2));
    }
}
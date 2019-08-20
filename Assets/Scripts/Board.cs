using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject TilePrefab;
    public int BoardLenght;
    public int BoardHight;
    GameObject AddName;

    // Start is called before the first frame update
    void Start()
    {
        //create the board
        for (int length = 0; length < BoardLenght; length++)
        {
            for (int hight = 0; hight < BoardHight; hight++)
            {
                AddName = (GameObject)Instantiate(TilePrefab, new Vector3((length * 1.1f)-(BoardLenght/2), 0, hight * 1.1f), Quaternion.identity);
                AddName.transform.parent = this.transform;
                AddName.name = "Tile_" + length + "_"+hight;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

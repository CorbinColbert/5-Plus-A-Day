using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class Board : MonoBehaviour
{
    public GameObject TilePrefab;
    public int BoardLenght;
    public int BoardHight;
    GameObject TempObject;
    public GameObject[,] BoardArray;

    // Start is called before the first frame update
    void Start()
    {
        BoardArray = new GameObject[BoardLenght, BoardHight];
        //create the board
        for (int length = 1; length <= BoardLenght; length++)
        {
            for (int height = 1; height <= BoardHight; height++)
            {
                TempObject = (GameObject)Instantiate(TilePrefab, new Vector3((length * 1.1f)-(BoardLenght/2.0f), 0, height * 1.1f), Quaternion.identity);
                TempObject.GetComponent<Tile>().XTilePos = length;
                TempObject.GetComponent<Tile>().YTilePos = height;
                TempObject.transform.parent = this.transform;
                TempObject.name = "Tile_" + length + "_"+height;
                BoardArray[length - 1, height - 1] = TempObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

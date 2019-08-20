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
            for (int hight = 1; hight <= BoardHight; hight++)
            {
                TempObject = (GameObject)Instantiate(TilePrefab, new Vector3((length * 1.1f)-(BoardLenght/2.0f), 0, hight * 1.1f), Quaternion.identity);
                TempObject.GetComponent<Tile>().XTilePos = length;
                TempObject.GetComponent<Tile>().YTilePos = hight;
                TempObject.transform.parent = this.transform;
                TempObject.name = "Tile_" + length + "_"+hight;
                BoardArray[length - 1, hight - 1] = TempObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGen : MonoBehaviour
{
    public GameObject TilePrefab;
    public int BoardLength;
    public int BoardHeight;
    GameObject TempObject;
    public GameObject[,] BoardArray;
    private int boardColourIndex = 0;
    public Material lightTile;
    public Material darkTile;

    // Start is called before the first frame update
    void Start()
    {
        BoardArray = new GameObject[BoardLength, BoardHeight];
        //create the board
        for (int height = 0; height < BoardHeight; height++)
        {
            for (int length = 0; length < BoardLength; length++)
            {
                TempObject = (GameObject)Instantiate(TilePrefab, new Vector3((length * 1) - ((BoardLength / 2) - 0.5f), 0, (height * 1) - ((BoardHeight / 2) - 0.5f)), Quaternion.identity);
                //TempObject.GetComponent<Tile>().xTilePos = length + 1;
                //TempObject.GetComponent<Tile>().yTilePos = height + 1;
                TempObject.transform.parent = this.transform;
                TempObject.name = "SnapPoint_" + (length + 1) + "_" + (height + 1);
                //TempObject.name = "Tile_" + (length + 1) + "_" + (height + 1);

                //BoardArray[length, height] = TempObject;

                //if (boardColourIndex % 2 == 0)
                //{
                //    TempObject.GetComponent<Renderer>().material = darkTile;
                //}
                //else
                //{
                //    TempObject.GetComponent<Renderer>().material = lightTile;
                //}

                //boardColourIndex++;
            }
            //boardColourIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

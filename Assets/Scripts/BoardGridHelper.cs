using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGridHelper : MonoBehaviour
{
    private GameObject[] boardArray;
    public GameObject[,] Board2DArray;
    public int BoardLength;
    public int BoardHeight;

    private void Start()
    {
        boardArray = gameObject.GetComponent<PremadeBoard>().BoardArray;

        Board2DArray = new GameObject[BoardHeight, BoardLength];
        int index = 0;
        //add elements from BoardArray to Board2DArray
        for (int i = 0; i < BoardHeight; i++)
        {
            for (int j = 0; j < BoardLength; j++)
            {
                Board2DArray[i, j] = boardArray[index];
                Board2DArray[i, j].GetComponent<Tile>().xTilePos = j + 1;
                Board2DArray[i, j].GetComponent<Tile>().yTilePos = i + 1;
                index++;
            }
        }
    }
}

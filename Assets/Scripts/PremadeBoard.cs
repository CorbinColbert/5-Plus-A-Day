using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremadeBoard : MonoBehaviour
{
    public GameObject[] BoardArray;
    private GameObject[,] Board2DArray;

    private void Start()
    {
        Board2DArray = new GameObject[8, 12];
        int index = 0;
        //add elements from BoardArray to Board2DArray
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                Board2DArray[i, j] = BoardArray[index];
                index++;
            }
        }
    }
}

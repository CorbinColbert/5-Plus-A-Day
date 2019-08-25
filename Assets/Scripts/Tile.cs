using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int xTilePos;
    public int yTilePos;
    private Material tileMaterial;


    // Start is called before the first frame update
    void Start()
    {
        tileMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectedHelper()
    {

    }
    //private void OnMouseEnter()
    //{
    //    gameObject.GetComponent<Renderer>().material = selectedTile;
    //}

    //private void OnMouseExit()
    //{
    //    gameObject.GetComponent<Renderer>().material = tileMaterial;
    //}
}

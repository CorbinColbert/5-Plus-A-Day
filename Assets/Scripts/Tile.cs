using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int xTilePos;
    public int yTilePos;
    private Material tileMaterial;
    public Material selectedTile;


    // Start is called before the first frame update
    void Start()
    {
        tileMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material = selectedTile;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material = tileMaterial;
    }
}

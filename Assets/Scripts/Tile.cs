using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xTilePos;
    public int yTilePos;
    
    private Material tileMaterial;
    [SerializeField]
    private Material selectedTileMaterial;

    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        tileMaterial = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelected(bool state) {
        selected = state;

        print("state changed");

        if (selected) {
            gameObject.GetComponent<Renderer>().material = selectedTileMaterial;
        } else {
            gameObject.GetComponent<Renderer>().material = tileMaterial;
        }
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

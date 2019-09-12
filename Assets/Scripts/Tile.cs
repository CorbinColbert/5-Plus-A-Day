using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static float hoverDistance = 0.6f;
    private bool selectable;

    public int xTilePos;
    public int yTilePos;
    
    private Material tileMaterial;
    [SerializeField]
    private Material selectedTileMaterial;

    private bool selected;
    public Unit unit;

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

        if (selected) {
            gameObject.GetComponent<Renderer>().material = selectedTileMaterial;
        } else {
            gameObject.GetComponent<Renderer>().material = tileMaterial;
        }
    }

    public void SetUnit(Unit unit) {
        this.unit = unit;
    }

    public void selectedHelper()
    {

    }

    public bool IsSelectable() {
        return selectable;
    }

    public void SetSelectable(bool selectable) {
       this.selectable = selectable;
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

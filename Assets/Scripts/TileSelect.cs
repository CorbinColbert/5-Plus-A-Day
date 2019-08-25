using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    public Material selectedTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    
    //while ray is hitting a tile collider
    //change its material to selected tile
    //when ray stops hitting the tile collider set back to original material
}

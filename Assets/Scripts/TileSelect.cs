using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        //if (Physics.Raycast(ray, out hit))
        //{
        //    hit.collider.gameObject.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<Tile>().selectedTile;
        //}
    }
}

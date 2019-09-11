using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attatch this to the camera in the scene
public class TileSelect : MonoBehaviour
{
    Tile currentlySelected = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 80.0f)) {
                if (hit.transform != null) {
                    print("Hit "+hit.transform.gameObject.name);
                    Tile other = null;
                    hit.transform.gameObject.TryGetComponent<Tile>(out other);
                    if (other != null) {
                        if (currentlySelected != null) {
                            currentlySelected.SetSelected(false);
                        }
                        currentlySelected = other;
                        currentlySelected.SetSelected(true);
                    }
                }
            }
        }
    }
}

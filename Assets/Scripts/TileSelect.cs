using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attatch this to the camera in the scene
public class TileSelect : MonoBehaviour
{
    Tile selectedTile = null;
    public GameObject alliedUnitPrefab = null;
    public GameObject enemyUnitPrefab = null;

    void Start() {

    }

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
                        if (selectedTile != null) {
                            selectedTile.SetSelected(false);
                        }
                        selectedTile = other;
                        selectedTile.SetSelected(true);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            if (selectedTile != null) {
                selectedTile.SetSelected(false);
                selectedTile = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) {
            if (selectedTile != null) {
                if (selectedTile.HasUnit() == false) {
                    GameObject unitPrefab = (Input.GetKeyDown(KeyCode.Alpha1)) ? alliedUnitPrefab: enemyUnitPrefab;
                    GameObject unit = Instantiate(unitPrefab, selectedTile.transform.position, selectedTile.transform.rotation);
                    Vector3 pos = new Vector3(unit.transform.position.x, unit.transform.position.y + (Tile.hoverDistance), unit.transform.position.z);
                    unit.transform.position = pos;
                    unit.GetComponent<Unit>().placeOnBoard(selectedTile);
                }                
            }
        }
    }
}

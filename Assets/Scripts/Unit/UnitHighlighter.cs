using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlighter : MonoBehaviour
{
    [SerializeField]
    private Material highlightMaterial;
    private Material originalMaterial;
    private bool isHighlightingTarget = false;

    void Update()
    {
        //When LMB clicked
        if (Input.GetMouseButtonDown(0)) {

            //Shoot ray from camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;

                //If unit is hit with ray
                if (objectHit.gameObject.TryGetComponent<Unit>(out Unit unit)) {

                    //If the unit has a target
                    if (unit.target != null) {
                        
                    }
                }
            }
        }
    }
}

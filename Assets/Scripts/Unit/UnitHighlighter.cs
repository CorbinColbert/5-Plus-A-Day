using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlighter : MonoBehaviour
{
    [SerializeField]
    private Material highlightMaterial;
    private bool isHighlightingTarget = false;

    void Update()
    {
        //print("Highlight Update()");

        //When LMB clicked
        if (Input.GetMouseButtonDown(0)) {
            //Shoot ray from camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                //If unit is hit with ray
                if (objectHit.gameObject.TryGetComponent<Unit>(out Unit unit)) {
                    TryHighlightTargetOf(unit);
                }
            }
        }
    }

    void TryHighlightTargetOf(Unit unit) {
        if (unit.target != null) {
            Material targetMaterial = unit.target.GetComponent<Renderer>().material;

            if (!unit.target.TryGetComponent<Highlight>(out Highlight highlight)) {
                highlight = unit.target.AddComponent<Highlight>();
                highlight.PerformHighlight(targetMaterial, highlightMaterial, 1.0f);
            }
        }
    }
    
}

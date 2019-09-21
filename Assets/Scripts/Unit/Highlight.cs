using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Material originalMaterial;
    private Material highlightMaterial;

    public void PerformHighlight(Material original, Material highlight, float delay) {
        this.originalMaterial = original;
        this.highlightMaterial = highlight;

        gameObject.GetComponent<Renderer>().material = highlightMaterial;

        Invoke("RevertHighlight", delay);

        Destroy(this, delay+0.5f);
    }

    void RevertHighlight() {
        gameObject.GetComponent<Renderer>().material = originalMaterial;
    }
}
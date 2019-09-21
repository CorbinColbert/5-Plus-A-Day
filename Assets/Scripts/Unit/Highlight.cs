using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Material originalMaterial;
    private float counter;
    private float duration;
    private bool running;

    void Start() {
        running = false;
    }

    void Update()
    {
        if (running) {
            counter = counter + Time.deltaTime;
            if (counter >= duration) {
                //Return material to original
                gameObject.GetComponent<Renderer>().material = originalMaterial;
            }
        }
        
    }

    public void PerformHighlight(Material material, float duration) {
        originalMaterial = gameObject.GetComponent<Renderer>().material;

        running = true;
    }
}
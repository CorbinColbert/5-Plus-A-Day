using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    Text percentageText;

    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text> ();
    }

    // Update is called once per frame
    public void updateSliderSize(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}

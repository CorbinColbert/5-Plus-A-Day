using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private Text gameText;

    void Start()
    {
        gameText = GetComponent<Text>();
    }

    void Update()
    {
        gameText.text = ""+GameManager.currency;
    }
}

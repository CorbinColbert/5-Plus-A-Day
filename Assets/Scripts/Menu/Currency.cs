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

    // Update is called once per frame
    void Update()
    {
        gameText.text = "Currency: $" + GameManager.currency;
    }
}

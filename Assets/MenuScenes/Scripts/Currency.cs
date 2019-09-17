using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private Text gameText;
    private static int currency;

    void Start()
    {
        gameText = GetComponent<Text>();

        //sets the currency value to 0
        currency = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            currency += 5;
        }
        else
        {
            Debug.Log("Wtf");
        }

        gameText.text = "Currency: $" + currency;
    }
}

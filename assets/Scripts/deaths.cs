using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deaths : MonoBehaviour
{
    public TextMeshProUGUI deathTxt;
    // Start is called before the first frame update
    void Start()
    {
        if (gameStats.Deaths == 1)
        {
            deathTxt.SetText("You died " + (gameStats.Deaths) + " time.");
        }
        else
            deathTxt.SetText("You died  " + (gameStats.Deaths) + " times.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tries : MonoBehaviour
{
    public TextMeshProUGUI triesTxt;
    // Start is called before the first frame update
    void Start()
    {
        if (gameStats.Deaths == 0) {
            triesTxt.SetText("It took you " + (gameStats.Deaths+1) + " try.");
        }
        else
            triesTxt.SetText("It took you " + (gameStats.Deaths+1) + " tries.");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

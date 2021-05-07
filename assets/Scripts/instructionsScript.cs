using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionsScript : MonoBehaviour
{

    public GameObject instructionsPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openInstructions()
    {
        instructionsPanel.SetActive(true);
    }
    public void closeInstructions()
    {
        instructionsPanel.SetActive(false);
    }
}

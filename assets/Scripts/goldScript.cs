using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldScript : MonoBehaviour
{

    public GameObject explosion; // drag your explosion prefab here

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject); // destroy the gold
            Destroy(expl, 0.5f); // delete the explosion after 3 seconds
        }
    }
}

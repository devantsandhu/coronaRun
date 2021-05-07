using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bombScript : MonoBehaviour
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(col.gameObject);
            gameStats.Deaths++;
            GetComponent<SpriteRenderer>().enabled = false; // hide the bomb
            Destroy(expl, 1); // delete the explosion after 3 seconds

            StartCoroutine(WaitForIt(1.0f));
        }
    }

    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("gameOVer");
    }
}

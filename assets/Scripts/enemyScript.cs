using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour
{
    public Vector3 startPosition;
    public List<Vector3> moveToPoints;
    public Vector3 currentPoint;

    public GameObject explosion; // drag your explosion prefab here
    public float moveSpeed;

    public int pointSelection;

    // Use this for initialization
    void Start()
    {

        //Sets the object to your starting point
        this.transform.position = startPosition;

    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }


    void Move()
    {

        //Starts to move the object towards the first "moveToPoint" you set in inspector
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint, Time.deltaTime * moveSpeed);

        //check to see if the object is at the next "moveToPoint"
        if (this.transform.position == currentPoint)
        {

            //if so it sets the next moveTo location
            pointSelection++;

            //if your object hits the last "moveToPoint it sends the object back to starting position to start the sequence over
            if (pointSelection == moveToPoints.Count)
            {
                pointSelection = 0;

            }

            //sets the destination of the "moveToPoint" destination
            currentPoint = moveToPoints[pointSelection];
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(col.gameObject);
            gameStats.Deaths++;
            //GetComponent<SpriteRenderer>().enabled = false; 
            Destroy(expl, 1);

            StartCoroutine(WaitForIt(1.0f));
        }
    }
    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("gameOVer");
    }
}

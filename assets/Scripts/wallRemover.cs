using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRemover : MonoBehaviour
{

    public Vector3 startPosition;
    public List<Vector3> moveToPoints;
    public Vector3 currentPoint;

    public float moveSpeed;
    public int pointSelection;

    // Start is called before the first frame update
    void Start()
    {
        pointSelection = 0;
        //currentPoint = moveToPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {

        //Starts to move the object towards the first "moveToPoint" you set in inspector
        transform.position = Vector3.MoveTowards(transform.position, currentPoint, Time.deltaTime * moveSpeed);
        currentPoint = moveToPoints[pointSelection];
        //check to see if the object is at the next "moveToPoint"
        if (transform.position == currentPoint)
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
        if (transform.position == moveToPoints[moveToPoints.Count - 1])
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "wallR" || col.gameObject.name == "wallL" || col.gameObject.name == "wallU" || col.gameObject.name == "wallD")
        {
            Destroy(col.gameObject);
        }
    }
}

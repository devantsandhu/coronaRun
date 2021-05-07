using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    private float moveinput;

    private Rigidbody2D rb;

    //private bool faceingRight = true;

   // public float checkRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /*
    private void FixedUpdate()
    {
        moveinput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveinput * speed, rb.velocity.y);

        if (faceingRight == false && moveinput > 0)
        {
            Flip();
        }
        else if (faceingRight == true && moveinput < 0)
        {
            Flip();
        }

    }
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * speed;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = Vector2.down * speed;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = Vector2.right * speed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = Vector2.left * speed;
        }
    }
    /*
    void Flip()
    {
        faceingRight = !faceingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    */
}


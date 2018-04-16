using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInput : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody rb;

    private bool movingUp, movingDown, movingLeft, movingRight;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        movementSpeed = 5f;

        movingDown = true;
        movingUp = false;
        movingLeft = false;
        movingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        FlyMomentum();
        FlyMovement();
    }

    void FlyMomentum()
    {
        if (movingDown == true)
        {
            transform.Translate(-Vector3.up * (movementSpeed * Time.deltaTime));

            //rb.AddForce(-Vector3.up * (movementSpeed * Time.deltaTime));
        }

        if (movingUp == true)
        {
            transform.Translate(Vector3.up * (movementSpeed * Time.deltaTime));

            //rb.AddForce(Vector3.up * (movementSpeed * Time.deltaTime));
        }

        if (movingLeft == true)
        {
            transform.Translate(-Vector3.right * (movementSpeed * Time.deltaTime));

            //rb.AddForce(-Vector3.right * (movementSpeed * Time.deltaTime));
        }

        if (movingRight == true)
        {
            transform.Translate(Vector3.right * (movementSpeed * Time.deltaTime));

            //rb.AddForce(Vector3.right * (movementSpeed * Time.deltaTime));
        }
    }

    void FlyMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            movingDown = false;
            movingLeft = false;
            movingRight = false;

            movingUp = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            movingUp = false;
            movingLeft = false;
            movingRight = false;

            movingDown = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            movingUp = false;
            movingDown = false;
            movingRight = false;

            movingLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            movingUp = false;
            movingDown = false;
            movingLeft = false;

            movingRight = true;
        }
    }
}

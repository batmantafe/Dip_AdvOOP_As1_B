using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class FlyInput : NetworkBehaviour
{
    [Header("Fly Movement")]
    public float movementSpeed;
    public Rigidbody rb;

    // Private Variables
    private bool movingUp, movingDown, movingLeft, movingRight;

    #region Unity Functions
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        movementSpeed = 5f;

        if (isLocalPlayer)
        {
            movingDown = false;
            movingUp = false;
            movingLeft = false;
            movingRight = false;

            RandomStartMovement();
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            FlyMomentum();
            FlyMovement();
        }
    }
    #endregion

    #region Movements
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

    void RandomStartMovement()
    {
        int randomMove = Random.Range(0,4);

        switch (randomMove)
        {
            case 0:
                movingUp = true;
                break;

            case 1:
                movingDown = true;
                break;

            case 2:
                movingLeft = true;
                break;

            case 3:
                movingRight = true;
                break;
        }
    }
    #endregion

    #region Collisions
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Up Wall":
                movingUp = false;
                movingLeft = false;
                movingRight = false;

                movingDown = true;
                break;

            case "Down Wall":
                movingDown = false;
                movingLeft = false;
                movingRight = false;

                movingUp = true;
                break;

            case "Left Wall":
                movingUp = false;
                movingDown = false;
                movingLeft = false;

                movingRight = true;
                break;

            case "Right Wall":
                movingUp = false;
                movingDown = false;
                movingRight = false;

                movingLeft = true;
                break;
        }
    }
    #endregion
}

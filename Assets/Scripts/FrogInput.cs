using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogInput : MonoBehaviour
{
    public float rotateSpeed;

    public GameObject tongueLength;
    public float tongueShotSeconds;

    private bool tongueIsShooting;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;

        rotateSpeed = 20f;
        tongueShotSeconds = .25f;

        tongueIsShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotatingFrogHead();

        ShootTongue();
    }

    void RotatingFrogHead()
    {
        if (Input.GetKey(KeyCode.A) && tongueIsShooting == false)
        {
            transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D) && tongueIsShooting == false)
        {
            transform.Rotate(-Vector3.forward * (rotateSpeed * Time.deltaTime));
        }
    }

    void ShootTongue()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            StartCoroutine(TongueShotTimer());
        }
    }

    IEnumerator TongueShotTimer()
    {
        tongueIsShooting = true;
        tongueLength.SetActive(true);

        yield return new WaitForSeconds(tongueShotSeconds);

        tongueIsShooting = false;
        tongueLength.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogInput : MonoBehaviour
{
    // Nested Class
    public class LifeTime
    {
        public float lifeSeconds;

        public LifeTime(float life)
        {
            lifeSeconds = life;
        }
    }

    [Header("Turn")]
    public float rotateSpeed;

    [Header("Tongue")]
    public GameObject tongueLength;
    public float tongueShotSeconds;
    
    // Calling the LifeTime nested class:
    // --Create instance of LifeTime in FrogInput called frogLife.
    // --Set lifeSeconds (=life) to 5f.
    public LifeTime frogLife = new LifeTime(10f);

    // Private Variables
    private bool tongueIsShooting;

    #region Unity Functions
    void Start()
    {
        Cursor.visible = false;

        rotateSpeed = 20f;
        tongueShotSeconds = 1f;

        tongueIsShooting = false;
    }

    void Update()
    {
        RotatingFrogHead();

        ShootTongue();

        LifeCountdown();
    }
    #endregion

    #region Turn Input
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
    #endregion

    #region Tongue Action
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
    #endregion

    #region
    void LifeCountdown()
    {
        frogLife.lifeSeconds = frogLife.lifeSeconds - (1 * Time.deltaTime);

        if (frogLife.lifeSeconds <= 0f)
        {
            frogLife.lifeSeconds = 0f;
        }

        Debug.Log(frogLife.lifeSeconds);
    }
    #endregion
}

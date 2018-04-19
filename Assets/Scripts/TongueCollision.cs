using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class TongueCollision : NetworkBehaviour
{
    public float flyTime;

    void Start()
    {
        flyTime = 5f;
    }

    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        // Kick FLY if hit by TONGUE
        if (other.gameObject.name == "Body")
        {
            Debug.Log(other.gameObject);

            other.transform.parent.gameObject.GetComponent<NetworkIdentity>().connectionToServer.Disconnect();

            FrogInput.frogLife.lifeSeconds = FrogInput.frogLife.lifeSeconds + flyTime;
        }
    }
}

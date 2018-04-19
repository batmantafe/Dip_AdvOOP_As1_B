using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class TongueCollision : NetworkBehaviour
{


    void Start()
    {

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

            other.transform.parent.gameObject.GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
        }
    }
}

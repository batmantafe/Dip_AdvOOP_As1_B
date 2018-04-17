using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("Spawning as Frog")]
    public FlyInput flyScript;
    public FrogInput frogScript;
    public GameObject frogSpawnPoint, frogGO;

    #region Unity Functions
    void Start()
    {
        frogSpawnPoint = GameObject.Find("Spawn 01 (Frog)");

        if (frogSpawnPoint.transform.position == new Vector3(0,-3.5f,7))
        {
            FrogSpawn();
        }
    }

    void Update()
    {

    }
    #endregion

    #region Spawn As Frog
    void FrogSpawn()
    {
        frogSpawnPoint.transform.position = new Vector3(-7,3.5f,0);

        flyScript = GetComponent<FlyInput>();
        frogScript = GetComponent<FrogInput>();
        frogGO = GameObject.Find("Frog GO");
        
        flyScript.enabled = false;
        frogScript.enabled = true;
        frogGO.GetComponent<FrogInput>().enabled = true;

        // Make Frog-Player child of Frog GameObject
        transform.parent = frogGO.transform;
    }
    #endregion
}

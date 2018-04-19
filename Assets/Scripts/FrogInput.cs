using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FrogInput : NetworkBehaviour
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
    public GameObject tongueLengthGO;
    public float tongueShotSeconds;
    public GameObject tonguePrefab;
    public Transform tongueSpawn;
    public float tongueShotCooldown;

    [Header("Frog's Setup")]
    public GameObject frogGO;
    public bool frogCanWin;

    [Header("Network Manager")]
    public GameObject networkManager;

    // Calling the LifeTime nested class:
    // --Create instance of LifeTime in FrogInput called frogLife.
    // --Set lifeSeconds (=life) to whatever-float.
    public static LifeTime frogLife = new LifeTime(10f);

    // Private Variables
    private bool tongueIsShooting;

    #region Unity Functions
    void Start()
    {
        Cursor.visible = false;

        if (isLocalPlayer)
        {
            FrogSetup();

            rotateSpeed = 20f;
            tongueShotSeconds = 0.25f;
            tongueShotCooldown = 1f;

            tongueIsShooting = false;

            frogCanWin = false;
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            RotatingFrogHead();

            ShootTongue();

            // Flies are in the game, Frog can now play to win
            if (NetworkServer.connections.Count >= 2)
            {
                frogCanWin = true;

                LifeCountdown();
            }

            // If no flies currently in the game BUT there WERE flies in the game earlier, then frog wins cos frog ate all flies
            // DEBUG: Note that NetworkServer.connections.Count does not go down when a Client disconnects because the Count still saves a Null entry for that Client.
            // DEBUG (cont.): Need to have a Function that creates a List from Count, then checks it for Null entries, then creates a new List that contains no Nulls,
            // DEBUG (cont.): ...like the C#Features Tower Defence script List that adds nearest Enemies and removes destroyed Enemies.
            if (NetworkServer.connections.Count <= 1 && frogCanWin == true)
            {
                Debug.Log("FROG wins");
            }
        }

        if (isServer)
        {
            Debug.Log("Players are: " + NetworkServer.connections.Count);
        }
    }
    #endregion

    #region Turn Input
    void RotatingFrogHead()
    {
        if (Input.GetKey(KeyCode.A) && tongueIsShooting == false)
        {
            frogGO.transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D) && tongueIsShooting == false)
        {
            frogGO.transform.Rotate(-Vector3.forward * (rotateSpeed * Time.deltaTime));
        }
    }
    #endregion

    #region Tongue Action
    void ShootTongue()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && tongueIsShooting == false)
        {
            CmdTongueShot();
        }
    }

    IEnumerator TongueShotTimer()
    {
        tongueIsShooting = true;

        GameObject tonguer = (GameObject)Instantiate(tonguePrefab, tongueSpawn.position, tongueSpawn.rotation);
        NetworkServer.Spawn(tonguer);

        yield return new WaitForSeconds(tongueShotSeconds);

        Destroy(tonguer);

        yield return new WaitForSeconds(tongueShotCooldown);

        tongueIsShooting = false;
    }

    [Command]
    void CmdTongueShot()
    {
        StartCoroutine(TongueShotTimer());
    }
    #endregion

    #region Frog's Life Timer
    void LifeCountdown()
    {
        frogLife.lifeSeconds = frogLife.lifeSeconds - (1 * Time.deltaTime);

        // IF time runs out AND still flies in the game THEN frog loses
        if (frogLife.lifeSeconds <= 0.1f && NetworkServer.connections.Count >= 2)
        {
            Debug.Log("FROG dies!");

            frogLife.lifeSeconds = 10f;

            Network.Disconnect();
            MasterServer.UnregisterHost();

            SceneManager.LoadScene("Lobby");
        }

        Debug.Log(frogLife.lifeSeconds);
    }
    #endregion

    #region Frog Setup
    void FrogSetup()
    {
        frogGO = GameObject.Find("Frog GO");
        tongueLengthGO = GameObject.Find("Length GO");

        tongueSpawn = GameObject.Find("TongueSpawn").transform;

        networkManager = GameObject.Find("NetworkManager");
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingleTon<PlayerManager> {

    int noofPlayers = 2;
    public Player[] playersArray;

    [SerializeField]
    Transform[] playerSpawnPoints;

    PlayerInputHandler playerInputHandler;

    public GameObject playerObject;

    void CreatePlayers()
    {
        // Create player prefabs here.. 

        // assign player commands to local variables here..
        for(int i = 0; i < noofPlayers; i++)
        {
            GameObject player = Instantiate(playerObject, playerSpawnPoints[i].position, Quaternion.identity);
            playersArray[i] = player.GetComponent<Player>();
        }

        AssignControls();
    }

    void AssignControls()
    {
        ControlsContainer.Instance.Init();
        playerInputHandler = new PlayerInputHandler();
    }

    // Use this for initialization
    void Awake ()
    {
        playersArray = new Player[noofPlayers];
        CreatePlayers();

        
    }
	
	// Update is called once per frame
	void Update () {
        //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(kcode))
        //        Debug.Log("KeyCode down: " + kcode +"    "+(int) kcode);
        //}

        if(playerInputHandler != null)
            playerInputHandler.Update();

    }

    public void UpdatePlayerScore(int playerIndex, int score)
    {
        playersArray[playerIndex].score += score;

        if (playersArray[playerIndex].score < 0)
            playersArray[playerIndex].score = 0;
    }

    public void UpdatePlayerTime(int playerIndex, float time)
    {
        playersArray[playerIndex].time += time;
    }

    public void UpdatePlayerSpeed(int playerIndex, float speed)
    {
        playersArray[playerIndex].speed += speed;
    }
}

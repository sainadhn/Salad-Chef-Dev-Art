using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float time;
    public int score;

    int playerIndex;
    Queue pickedItems;

    Vector3 direction;
   public float speed;

    CharacterController characterController;

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }

        set
        {
            playerIndex = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        direction = Vector3.zero;
        characterController = transform.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        characterController.Move(direction * speed);
    }
#region PlayerMovement
    public void MoveForward()
    {
        direction.x = 0;
        direction.y = 1;
    }

    public void MoveDown()
    {
        direction.x = 0;
        direction.y = -1;
    }

    public void MoveLeft()
    {
        direction.x = -1;
        direction.y = 0;
    }
    public void MoveRight()
    {
        direction.x = 1;
        direction.y = 0;
    }

    public void OnAnyKeyReleased()
    {
        direction.x = 0;
        direction.y = 0;
    }
#endregion
}

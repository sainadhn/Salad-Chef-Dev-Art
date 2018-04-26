using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler
{
    public void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        foreach(KeyValuePair<KeyCode, Command> val in ControlsContainer.Instance.controlls)
        {
            if (Input.GetKeyDown(val.Key))
                val.Value.Execute();

            if (Input.GetKeyUp(val.Key))
                val.Value.OnKeyReleased();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsContainer
{
    public Dictionary<KeyCode, Command> controlls;

    static ControlsContainer instance;

    public static ControlsContainer Instance
    {
        get
        {
            if (instance == null)
                instance = new ControlsContainer();

            return instance;
        }
    }

    private ControlsContainer()  {    }

    public void Init()
    {
        controlls = new Dictionary<KeyCode, Command>();
        #region Player1Controls
        Player p0 = PlayerManager.Instance.playersArray[0];

        int kCode = PlayerPrefs.GetInt("P1MoveForward", 119); // value for w
        controlls.Add((KeyCode)kCode, new MoveForward(p0));

        kCode = PlayerPrefs.GetInt("P1MoveLeft", 97); // value for a
        controlls.Add((KeyCode)kCode, new MoveLeft(p0));

        kCode = PlayerPrefs.GetInt("P1MoveRight", 100); // value for d
        controlls.Add((KeyCode)kCode, new MoveRight(p0));

        kCode = PlayerPrefs.GetInt("P1MoveBack", 115); // value for s
        controlls.Add((KeyCode)kCode, new MoveDown(p0));

        kCode = PlayerPrefs.GetInt("P1PickItem", 113); // value for q
        controlls.Add((KeyCode)kCode, new PickItem(p0));

        kCode = PlayerPrefs.GetInt("P1DropItem", 101); // value for e
        controlls.Add((KeyCode)kCode, new DropItem(p0));

        kCode = PlayerPrefs.GetInt("P1Chop", 99); // value for c
        controlls.Add((KeyCode)kCode, new Chop(p0));

        kCode = PlayerPrefs.GetInt("P1Serve", 122); // value for z
        controlls.Add((KeyCode)kCode, new Serve(p0));

        kCode = PlayerPrefs.GetInt("P1ThrowInTrash", 120); // value for x
        controlls.Add((KeyCode)kCode, new ThrowInTrash(p0));
        #endregion

        #region Player2Controls
        Player p1 = PlayerManager.Instance.playersArray[1];

        kCode = PlayerPrefs.GetInt("P2MoveForward", 273); // value for up arrow
        controlls.Add((KeyCode)kCode, new MoveForward(p1));

        kCode = PlayerPrefs.GetInt("P2MoveLeft", 276); // value for left arrow
        controlls.Add((KeyCode)kCode, new MoveLeft(p1));

        kCode = PlayerPrefs.GetInt("P2MoveRight", 275); // value for right arrow
        controlls.Add((KeyCode)kCode, new MoveRight(p1));

        kCode = PlayerPrefs.GetInt("P2MoveBack", 274); // value for down arrow
        controlls.Add((KeyCode)kCode, new MoveDown(p1));

        kCode = PlayerPrefs.GetInt("P2PickItem", 277); // value for insert
        controlls.Add((KeyCode)kCode, new PickItem(p1));

        kCode = PlayerPrefs.GetInt("P2DropItem", 127); // value for Delete
        controlls.Add((KeyCode)kCode, new DropItem(p1));

        kCode = PlayerPrefs.GetInt("P2Chop", 13); // value for Enter/Return
        controlls.Add((KeyCode)kCode, new Chop(p1));

        kCode = PlayerPrefs.GetInt("P2Serve", 279); // value for End button
        controlls.Add((KeyCode)kCode, new Serve(p1));

        kCode = PlayerPrefs.GetInt("P2ThrowInTrash", 8); // value for back space
        controlls.Add((KeyCode)kCode, new ThrowInTrash(p1));
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Timer playerTime;
    Timer choppingTime;
    bool isChopping;

    public float ChoppingProgress;
    private float time;
    public int score;
    public float speed;

    int playerIndex;
    public List<int> pickedItems;

    Vector3 direction;

    int triggeredVegetableId;
    int triggeredCustomerId;

    CharacterController characterController;
    string saladCombinationString;

    TextMesh message;
    

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

    public float Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
            playerTime.currentTime = time;
        }
    }


    // Use this for initialization
    void Start ()
    {
        playerTime = new Timer(true);
        choppingTime = new Timer(true);
        pickedItems = new List<int>();
        direction = Vector3.zero;
        characterController = transform.GetComponent<CharacterController>();

        message = transform.Find("Message").GetComponent<TextMesh>();
        SetPlayerSpecificData();
    }

    public void SetPlayerSpecificData()
    {
        if (playerIndex == 1)
        {
            message.alignment = TextAlignment.Right;
            Material mat = GetComponent<MeshRenderer>().material;
            mat.color = Color.red;
            //GetComponent<MeshRenderer>().material = mat;
        }
        else
        {
            Material mat = GetComponent<MeshRenderer>().material;
            mat.color = Color.green;
        }

    }

#region Triggers
    private void OnTriggerEnter(Collider other)
    {
        KeyCode kCode;
        string message = "";
        switch (other.tag)
        {
            case "ChopBoard":
                // should reduce calls to player prefs..
                kCode = (KeyCode)PlayerPrefs.GetInt("P1Chop", 99);
                if(playerIndex == 1)
                    kCode = (KeyCode)PlayerPrefs.GetInt("P2Chop", 13);

                message = string.Format("Press \"{0}\" to chop vegetable", kCode.ToString());
                PlayerManager.Instance.ShowMessage(playerIndex, message);
                break;
            case "Vegetable":
                triggeredVegetableId = int.Parse(other.name);
                kCode = (KeyCode)PlayerPrefs.GetInt("P1PickItem", 113);
                KeyCode dropCode = (KeyCode)PlayerPrefs.GetInt("P1DropItem", 101);
                if (playerIndex == 1)
                {
                    kCode = (KeyCode)PlayerPrefs.GetInt("P2PickItem", 277);
                    dropCode = (KeyCode)PlayerPrefs.GetInt("P2DropItem", 127);
                }
                message = string.Format("Press \"{0}\" to pick the vegetable or \n \"{1}\" to drop vegetable", kCode.ToString(), dropCode.ToString());
                PlayerManager.Instance.ShowMessage(playerIndex, message);
                break;
            case "Customer":
                triggeredCustomerId = int.Parse(other.name);

                kCode = (KeyCode)PlayerPrefs.GetInt("P1Serve", 122);
                if (playerIndex == 1)
                    kCode = (KeyCode)PlayerPrefs.GetInt("P2Serve", 279);

                message = string.Format("Press \"{0}\" to serve the salad to customer", kCode.ToString());
                PlayerManager.Instance.ShowMessage(playerIndex, message);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string message = "";
        PlayerManager.Instance.ShowMessage(playerIndex, message);
    }
    #endregion

    public IEnumerator ShowMessage(string message, float duration = 5)
    {
        this.message.text = message;
        yield return new WaitForSeconds(duration);
        this.message.text = "";
    }
    // Update is called once per frame
    void Update ()
    {
        playerTime.Update();
        
        if (!isChopping)
            characterController.Move(direction * speed);
        else
        {
            choppingTime.Update();

            ChoppingProgress = choppingTime.currentTime / GameManager.Instance.gameData.vegetableDetails[triggeredVegetableId].timeToChop;
            if (ChoppingProgress == 0)
            {
                isChopping = false;
                saladCombinationString += triggeredVegetableId + ",";
                pickedItems.Remove(triggeredVegetableId);
            }
        }
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

    public void PickItem()
    {
        if (pickedItems.Count == 2)
            PlayerManager.Instance.ShowMessage(PlayerIndex, "Only 2 Vegetables can be picked.");
        else
            pickedItems.Add(triggeredVegetableId);
    }

    public void DropItem()
    {
        if (pickedItems.Count == 0)
            PlayerManager.Instance.ShowMessage(PlayerIndex, "No Vegetables available to drop");
        else
        {
            if(pickedItems.IndexOf(triggeredVegetableId) < 0)
                PlayerManager.Instance.ShowMessage(PlayerIndex, "This Vegetable to not picked, to drop.");
            else
                pickedItems.Remove(triggeredVegetableId);
        }
    }

    public void Chop()
    {
        if (pickedItems.Count > 0)
        {
            choppingTime.currentTime = GameManager.Instance.gameData.vegetableDetails[pickedItems[0] - 1].timeToChop;
            isChopping = true;
        }
        else
            PlayerManager.Instance.ShowMessage(PlayerIndex, "No Vegetables were picked to chop.");
    }

    public void Serve()
    {
        saladCombinationString = saladCombinationString.TrimEnd(',');
        CustomerManager.Instance.customers[triggeredCustomerId - 1].ServeSalad(playerIndex, saladCombinationString);
        ClearChoppingBoard();
    }

    public void ThrowInTrash()
    {
        pickedItems.Clear();
        PlayerManager.Instance.UpdatePlayerScore(playerIndex, -20);
    }

    void ClearChoppingBoard()
    {
        saladCombinationString = "";
    }
}

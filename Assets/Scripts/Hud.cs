using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text p1Score;
    public Text p1Time;
    public Text p1PickedVegetables;
    public Image p1ChoppingTime;

    public Text p2Score;
    public Text p2Time;
    public Text p2PickedVegetables;
    public Image p2ChoppingTime;

    string p1PickedVegNames;
    string p2PickedVegNames;
    // Update is called once per frame
    void Update ()
    {
        // player 1 details
        p1Score.text = PlayerManager.Instance.playersArray[0].score.ToString();
        p1Time.text = PlayerManager.Instance.playersArray[0].Time.ToString();

        p1PickedVegNames = "";
        for (int i = 0; i < PlayerManager.Instance.playersArray[0].pickedItems.Count; i++)
        {
            int itemId = PlayerManager.Instance.playersArray[0].pickedItems[i];
            p1PickedVegNames += GameManager.Instance.gameData.vegetableDetails[itemId - 1].name + ",";
        }
        p1PickedVegNames = p1PickedVegNames.TrimEnd(',');
        p1PickedVegetables.text = p1PickedVegNames;

        p1ChoppingTime.fillAmount = PlayerManager.Instance.playersArray[0].ChoppingProgress;

        // player 2 details
        p2Score.text = PlayerManager.Instance.playersArray[1].score.ToString();
        p2Time.text = PlayerManager.Instance.playersArray[1].Time.ToString();

        p2PickedVegNames = "";
        for (int i = 0; i < PlayerManager.Instance.playersArray[1].pickedItems.Count; i++)
        {
            int itemId = PlayerManager.Instance.playersArray[1].pickedItems[i];
            p2PickedVegNames += GameManager.Instance.gameData.vegetableDetails[itemId - 1].name + ",";
        }
        p2PickedVegNames = p2PickedVegNames.TrimEnd(',');
        p2PickedVegetables.text = p2PickedVegNames;

        p2ChoppingTime.fillAmount = PlayerManager.Instance.playersArray[1].ChoppingProgress;

    }
}

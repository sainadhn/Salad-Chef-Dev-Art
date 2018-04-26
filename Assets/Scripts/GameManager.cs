using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GameManager : SingleTon<GameManager> {

    public TextAsset saladJson;
    public TextAsset vegetablesJson;

    public GameData gameData;

    public GameObject HudUI;
	// Use this for initialization
	void Start ()
    {
        gameData = new GameData();

        // push salad details to game data..
        JSONNode node = JSON.Parse(saladJson.text);
        JSONArray saladArray = node.AsArray;

        gameData.saladDetails = new SaladDetails[saladArray.Count];

        int count = saladArray.Count;
        for (int i = 0; i < count; i++)
        {
            JSONNode n = saladArray[i];
            SaladDetails sd = new SaladDetails();
            sd.id = n["SaladId"].AsInt;
            sd.vegetables = n["VegCombinations"];
            sd.score = n["Score"].AsInt;

            gameData.saladDetails[i] = sd;
        }
        //..............

        // push salad details to game data..
        JSONNode vegNode = JSON.Parse(vegetablesJson.text);
        JSONArray vegArray = vegNode.AsArray;

        gameData.vegetableDetails = new VegetableDetails[vegArray.Count];

        count = vegArray.Count;
   
        for (int i = 0; i < count; i++)
        {
            JSONNode n = vegArray[i];
            VegetableDetails vd = new VegetableDetails();
            vd.id = n["VegetableId"].AsInt;
            vd.name = n["Name"];
            vd.timeToChop = n["TimeToChopInSeconds"].AsFloat;
            gameData.vegetableDetails[i] = vd;
        }
        //..............

    }

    public void EnableHud(bool isEnable)
    {
        HudUI.SetActive(isEnable);
    }
}

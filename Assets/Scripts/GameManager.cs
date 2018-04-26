using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GameManager : SingleTon<GameManager> {

    public TextAsset saladJson;
    public TextAsset vegetablesJson;

    public GameData gameData;
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
            vd.timeToChop = n["TimeToChopInSeconds"].AsFloat;
            Debug.Log("n   "+ n["VegetableId"] +"  timeToLeave " + n["TimeToChopInSeconds"]);
            gameData.vegetableDetails[i] = vd;
        }
        //..............

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public SaladDetails[] saladDetails;
    public VegetableDetails[] vegetableDetails;

}

public class SaladDetails
{
    public int id;
    public string vegetables;
    public int score;
}

public class VegetableDetails
{
    public int id;
    public float timeToChop;
}

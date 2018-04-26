using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool isAvailable = true;
    public bool isServed;
    public int saladIndex;
    Timer timer;
    float timeToLeave;
    float angryTimeFactor;
    string[] correctCombination;
    Transform timeLeft;
    Vector3 timeLeftScale;
    float clampTimeValue;
    float goalTimeToGetPickup;
    int pointsForWrongServe = -25;

    List<int> playersWhoMadeMeAngry;

    private void Awake()
    {
        isAvailable = true;
    }
    // Use this for initialization
    void Start ()
    {
        timer = new Timer(false);
        playersWhoMadeMeAngry = new List<int>();
        timeLeft = transform.Find("TimeLeft");
        timeLeftScale = timeLeft.localScale;
        goalTimeToGetPickup = 0.7f;
    }

    // Update is called once per frame
    void Update ()
    {
        timer.Update();
        clampTimeValue =  Mathf.Clamp(((timer.currentTime + angryTimeFactor) / timeToLeave), 0, 1);
        timeLeftScale.x = 1 - clampTimeValue;

        timeLeft.localScale = timeLeftScale;

        // Once the wait time is over..
        if (clampTimeValue >= 1 && !isServed)
        {
            for(int i = 0; i < playersWhoMadeMeAngry.Count; i++)
                PlayerManager.Instance.UpdatePlayerScore(playersWhoMadeMeAngry[i], pointsForWrongServe * 2);

            // display some effect to show points loosing..
            Reset();
        }
    }

    void Reset()
    {
        timeLeftScale.x = 0;
        timer.currentTime = 0;
        isServed = false;
        Array.Clear( correctCombination, 0, correctCombination.Length);
        angryTimeFactor = 0;
        isAvailable = true;
        playersWhoMadeMeAngry.Clear();
        CustomerManager.Instance.SpawnCustomersWithDelay(2);
        gameObject.SetActive(false);
    }

    public void SetSalad(int saladIndex)
    {
        this.saladIndex = saladIndex;
        string[] vegetables = GameManager.Instance.gameData.saladDetails[saladIndex].vegetables.Split(',');

        correctCombination = vegetables;
        isAvailable = false;

        float timeToChop = 0;
        for(int i = 0; i < vegetables.Length; i++)
        {
            int vegId = int.Parse(vegetables[i]);
            timeToChop += GameManager.Instance.gameData.vegetableDetails[vegId-1].timeToChop;
        }
        timeToLeave = timeToChop / 0.2f;// considering the total time to chop is only 20% of the customer time.
       
    }

    /// <summary>
    /// Called when the player is Triggered with customer to serve..
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="combination"></param>
    public void ServeSalad(int playerIndex, string combination)
    {
        bool isFedCorrectly = CheckVegiesCombination(combination);
        if (isFedCorrectly) // if served correct salad ..
        {
            isServed = true;

            PlayerManager.Instance.UpdatePlayerScore(playerIndex, GameManager.Instance.gameData.saladDetails[saladIndex].score);

            // if player served 70% before waiting time.
            if(clampTimeValue <= 0.3f)
            {
                // spawn a pick up..
            }

            Reset();
        }
        else // if served incorrect salad, minus some points
        {
            angryTimeFactor += 0.01f;
            PlayerManager.Instance.UpdatePlayerScore(playerIndex, pointsForWrongServe);

            // push only if this player is not exists..
            if (playersWhoMadeMeAngry.IndexOf(playerIndex) == -1)
                playersWhoMadeMeAngry.Add(playerIndex);
        }
    }
    
    bool CheckVegiesCombination(string combination)
    {
        string[] receivedCombination = combination.Split(',');

        if(receivedCombination.Length != correctCombination.Length)
        {
            // oops served wrong salad..
            return false;
        }

        for(int i = 0; i < receivedCombination.Length; i++)
        {
            if(Array.IndexOf(correctCombination, receivedCombination[i]) < 0) // if item not found.
            {
                // oops served wrong salad..
                return false;
            }
        }

        return true;
    }
}

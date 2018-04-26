using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float currentTime;
    bool countDownTimer;
    public Timer(bool isCountDownTimer)
    {
        countDownTimer = isCountDownTimer;
    }
    // Update is called once per frame
    public void Update ()
    {
        if (!countDownTimer)
            currentTime += Time.deltaTime;
        else
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                currentTime = 0;
        }
    }
}

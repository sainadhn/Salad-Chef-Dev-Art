using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float currentTime;

    // Update is called once per frame
    public void Update ()
    {
        currentTime += Time.deltaTime;
    }
}

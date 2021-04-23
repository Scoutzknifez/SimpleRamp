﻿using UnityEngine;
using UnityEngine.UI;

public class UpdateTimeDisplay : MonoBehaviour
{
    [SerializeField]
    private Text timeKeeper = null;

    public bool timerRunning = false;
    private float startTime = -1;
    private float endTime = -1;

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning && startTime != -1 && endTime != -1)
        {
            timeKeeper.text = ("Time: " + (endTime - startTime)).Substring(0, 10);
        }
        else if (timerRunning)
        {
            timeKeeper.text = ("Time: " + (Time.time - startTime)).Substring(0, 10);
        } else
        {
            timeKeeper.text = "Timer: Paused";
        }
    }

    public void startTimer()
    {
        timerRunning = true;
        startTime = Time.time;
    }

    public void endTimer()
    {
        timerRunning = false;
        endTime = Time.time;
    }

    public void resetTimer()
    {
        timerRunning = false;
        startTime = -1;
        endTime = -1;
    }
}
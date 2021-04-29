using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text timeKeeper = null;

    public bool timerRunning = false;

    private float startTime = -1;
    private float endTime = -1;

    [HideInInspector]
    public string time;

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            time = createTimeSection(startTime, Time.time);
            timeKeeper.text = ("Time: " + time).Substring(0, 10);
        }
        else if (!timerRunning && startTime != -1 && endTime != -1)
        {
            time = createTimeSection(startTime, endTime);
            timeKeeper.text = ("Time: " + time).Substring(0, 10);
        }
        else
        {
            timeKeeper.text = "Time: Paused";
        }
    }

    public string createTimeSection(float start, float end)
    {
        float time = end - start;
        string timeSpot = time + "";

        while (timeSpot.Length < 4)
        {
            timeSpot += " ";
        }

        if (timeSpot.Length > 4)
        {
            timeSpot = timeSpot.Substring(0, 4);
        }

        return timeSpot;
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

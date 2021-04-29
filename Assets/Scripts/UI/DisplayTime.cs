using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public Timer timer;
    public TMPro.TMP_Text display;

    // Update is called once per frame
    void Update()
    {
        display.text = timer.time;
    }
}

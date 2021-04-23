﻿using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject resolutionPanel;
    public GameObject menuPanel;

    public void goToResolutionPanel()
    {
        menuPanel.SetActive(false);
        resolutionPanel.SetActive(true);
    }

    public void goToMainMenu()
    {
        menuPanel.SetActive(true);
        resolutionPanel.SetActive(false);
    }
}
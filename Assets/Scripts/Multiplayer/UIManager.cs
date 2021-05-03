using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Multiplayer")]
    public TMP_InputField ipField;
    public TMP_InputField portField;
    public TMP_InputField usernameField;
    public string multiplayerLevelName = "Multiplayer";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void LoadMultiplayer()
    {
        usernameField.interactable = false;

        int port = -1;
        if (Int32.TryParse(portField.text.Trim(), out port))
        {
            Globals.IP = ipField.text.Trim();
            Globals.Port = port;
            Globals.username = usernameField.text.Trim();
            SceneManager.LoadScene(multiplayerLevelName);

        }
    }

    public void playSingleplayer()
    {
        SceneManager.LoadScene("Level_1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Meta data")]
    public int id;
    public MeshRenderer model;

    [Header("Known in game")]
    public string username;
    public TMP_Text usernameDisplay;
    public float maxHealth;
    public float health;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
    }

    private void Update()
    {
        if (usernameDisplay != null)
        {
            Vector3 lookTowards = Globals.localPlayer.GetComponentInChildren<Camera>().transform.position;
            
            // The forward vector of a TMP_Text is actually behind it.
            // Instead of doing a bunch of math to map the LookAt coordinate
            // on the reflection in 3D space using the usernameDisplay.transform
            // as an axis... we just changed the display to -1 X scale
            usernameDisplay.transform.LookAt(lookTowards);
        }
    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }
}

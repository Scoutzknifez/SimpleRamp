﻿using UnityEngine;

public class FPSMouseMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The player body to rotate as movement happens.")]
    private Transform playerBody = null;

    [SerializeField]
    [Tooltip("The mouse sensitivity.")]
    private float mouseSensitivity = 100f;

    float xRotation = 0f;

    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

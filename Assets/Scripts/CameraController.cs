using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
    }

    void Update()
    {
        float mouseY = -Input.GetAxis("Mouse Y");
        
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, transform.rotation.eulerAngles.y, 0.0f);
        transform.rotation = localRotation;
    }
}

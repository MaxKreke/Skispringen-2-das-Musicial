using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 2;
    public float verticalRotation = 0;
    public float horizontalRotation = 0;

    private void Update()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= inputY;
        horizontalRotation += inputX;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

    }
}

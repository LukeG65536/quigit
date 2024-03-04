using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;  ///bunch of variables for the player controller
    public float sens = 5f;

    public float xRotation;
    public float yRotation;

    public float xFinal;
    public float yFinal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LateUpdate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        float mouseX = Input.GetAxisRaw("Mouse X") * sens;  //basic camera movement
        float mouseY = Input.GetAxisRaw("Mouse Y") * sens;
        yRotation += mouseX;
        xRotation -= mouseY;
        xFinal = xRotation;
        yFinal = yRotation;
        xFinal = Mathf.Clamp(xFinal, -90f, 90f);
        transform.rotation = Quaternion.Euler(xFinal, yFinal, 0);
        player.transform.rotation = Quaternion.Euler(0, yFinal, 0); ;
    }
}
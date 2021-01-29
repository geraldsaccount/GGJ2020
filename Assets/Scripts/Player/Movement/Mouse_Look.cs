using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    [Header ("Input")]
    public float MouseX;
    public float MouseY;

    [Header ("Values")]
    public float MouseSensitivity;

    private float xRotation;

    public Transform PlayerBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}

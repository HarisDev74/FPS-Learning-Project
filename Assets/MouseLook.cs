using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Transform playerBody;
    public Transform playerCamera;


    public float mouseSensitivity = 100f;
    public float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        } else
        {
            transform.Rotate(Vector3.up * mouseX);
        }


    }
}

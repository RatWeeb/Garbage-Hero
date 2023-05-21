using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public float mouseSense;
    public Transform cam;

    private float xRotation;

    // Update is called once per frame
    void Update()
    {
        CameraWork();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CameraWork()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;
        // Multiplied by mousesense and time.delta because of update

        xRotation -= mouseY;

        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);


    }

}

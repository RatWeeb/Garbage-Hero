using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundDistance = 0.2f;
    public AudioSource moveSound;


    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 pastDir;
    private float currentJumpForce = 50f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // Calculate movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 movementDirection = (transform.right * horizontalInput + transform.forward * verticalInput).normalized;

        if (isGrounded)
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                moveSound.Play();
            }
            else
            {
                moveSound.Stop();
            }
            // Apply movement
            rb.MovePosition(rb.position + movementSpeed * movementDirection * Time.deltaTime);
        }
        else
        {
            moveSound.Stop();
            rb.MovePosition(rb.position + movementSpeed * pastDir * Time.deltaTime);
        }
        // Jumping
        if (isGrounded && Input.GetKey(KeyCode.Space) && currentJumpForce < jumpForce)
        {
            currentJumpForce += 50f * Time.deltaTime;
            Debug.Log("Adding "+ currentJumpForce);
        } 
        else if( (currentJumpForce > 50f && isGrounded) || currentJumpForce >= jumpForce)
        {
            Debug.Log(currentJumpForce);
            
            rb.AddForce(transform.up * currentJumpForce, ForceMode.Impulse);
            pastDir = movementDirection;
            currentJumpForce = 50f;
        }

        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        Camera mainCamera = Camera.main;
        mainCamera.transform.RotateAround(transform.position, transform.right, -mouseY);
    }

}

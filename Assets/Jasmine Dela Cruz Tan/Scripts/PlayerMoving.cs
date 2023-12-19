using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // Player speed
    public float speed = 4.0f;

    // Jump force 
    public float jumpForce = 8.0f;

    // Indicates whether gravity is inverted
    private bool GravityInverted = false;

    // Player Inputs
    private float horizontalInput;
    private float verticalInput;

    // Ground checking variables
    public float drag;
    public float playerHeight;
    public LayerMask GroundPresent;
    private bool grounded;

    private Rigidbody rb;
    private Vector3 moveDirection;
    public Transform orientation;

    private void Start()
    {
        // Get player rigidbody
        rb = GetComponent<Rigidbody>();
        // Stop player from rotating
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Check for ground using raycast with the layermask 'groundpresent'
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, GroundPresent);

        // Check for gravity inversion using 'g' key
        if (Input.GetKeyDown(KeyCode.G))
        {
            InvertGravity(); // Calls the InvertGravity function
        }
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (GravityInverted)
        {
            moveDirection = Quaternion.FromToRotation(Vector3.up, -Physics.gravity) * moveDirection;
        }

        // Apply force to the rigidbody
        rb.AddForce(moveDirection.normalized * speed * 3f, ForceMode.Force);
    }

    void InvertGravity()
    {
        GravityInverted = !GravityInverted;

        // Change gravity
        Physics.gravity = GravityInverted ? new Vector3(0f, -9.81f, 0f) : new Vector3(0f, 9.81f, 0f);

        // Check if the player is close to the ceiling or ground
        RaycastHit hit;
        float raycastDistance = 2f;
        Vector3 raycastDirection = GravityInverted ? Vector3.down : Vector3.up;

        if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance))
        {
            // Move player up or down 
            float distanceToSurface = raycastDistance - hit.distance;
            transform.position += raycastDirection * distanceToSurface;
        }
    }
}

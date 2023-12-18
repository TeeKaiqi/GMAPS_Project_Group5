using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform orientation;

    Rigidbody rb;
    Vector3 moveDirection;

    //player Inputs
    float horizontalInput;
    float verticalInput;

    //check if player is on the ground
    public float drag;

    //creating checking
    public float playerHeight;
    public LayerMask GroundPresent;
    bool grounded;

    //jump 
    public float jumpForce = 8.0f;

    //gravity
    bool isGravityInverted = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        horizontalInput = Input.GetAxis("Horizontal");  
        verticalInput = Input.GetAxis("Vertical");      
        // Check for ground using raycast 
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, GroundPresent);

        if (grounded)
        {
            rb.drag = drag;
            
            // jump
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
                
            }
            // Check for gravity inversion
            if (Input.GetKeyDown(KeyCode.G))
            {
                InvertGravity();
            }
        }
        else
        {
            rb.drag = 0;
        }
    }



    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * speed * 10f , ForceMode.Force);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void InvertGravity()
    {
        isGravityInverted = !isGravityInverted;

        // Enable or disable gravity based on the isGravityInverted flag
        rb.useGravity = !isGravityInverted;

        //rotate player 180
        if (isGravityInverted)
        {
            transform.Rotate(Vector3.forward, 180f);
        }
    }

}

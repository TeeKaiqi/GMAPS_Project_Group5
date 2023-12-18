using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    private RotationScript rotationScript;

    private float speed = 5f; //declare movement speed
    private float jumpForce = 8f; //declare jump force
    private bool isGrounded; //declare boolean that holds info on whether the character is grounded
    private Vector3 gravityDirection; //initial direction of gravity in the beginning and if the character is grounded.

    public Transform orientation; //the game object that has the information on the orientation of the character

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //get charactercontroller component
        rotationScript = GetComponent<RotationScript>(); //get rotationscript component
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); //Calls the function that handles all the movement
        CheckIfJump(); //Calls the function that checks if the space bar is pressed 
    }

    public void HandleMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");

        isGrounded = characterController.isGrounded;

        Debug.Log(characterController.isGrounded);

        if (!isGrounded)
        {
            gravityDirection = rotationScript.ForwardDirection(orientation); // Use the rotation script to get the forward direction
        }
        else
        {
            gravityDirection = Vector3.down; // Reset gravity direction when grounded
        }

        // Calculate the move direction in world space
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Apply speed
        characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

        // Apply gravity
        characterController.Move(gravityDirection * speed * Time.deltaTime);
    }

    public void CheckIfJump()
    {
        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
        }
    }
}

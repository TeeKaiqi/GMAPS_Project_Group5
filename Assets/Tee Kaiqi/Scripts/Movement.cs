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
    private float jumpForce = 15f; //declare jump force
    private bool isGrounded; //declare boolean that holds info on whether the character is grounded
    private Vector3 gravityDirection; //initial direction of gravity in the beginning and if the character is grounded.

    public Transform orientation; //the game object that has the information on the orientation of the character
    public LayerMask floorLayer;
    public Camera playerCamera;

    float horizontalInput;
    float verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //get charactercontroller component
        rotationScript = GetComponent<RotationScript>(); //get rotationscript component
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); //Calls the function that handles all the movement
        CheckIfJump(); //Calls the function that checks if the space bar is pressed 
    }

    public void HandleMovement()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, floorLayer);

        if (!isGrounded)
        {
            // Character is airborne
            Vector3 gravityDirection = playerCamera.transform.forward; // Opposite of camera's forward direction
            Debug.Log(gravityDirection);

            // Apply gravity in the opposite direction
            characterController.Move(gravityDirection * speed * Time.deltaTime);
        }
        else
        {
            // Character is grounded
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the move direction in world space
            Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // Apply speed
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }


    public void CheckIfJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float u = Mathf.Sqrt(-2 * Physics.gravity.y * jumpForce);
            Vector3 jumpVelocity = Vector3.up * u;
            characterController.Move(jumpVelocity * Time.deltaTime);
        }
    }
}

//public void HandleMovement()
//{
//    isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, floorLayer);
//    if (isGrounded)
//    {
//        Debug.Log("Character is grounded" + isGrounded);
//    }
//    else
//    {
//        Vector3 gravityDirection = -rotationScript.ForwardDirection(orientation); // Opposite of camera's forward direction

//        // Apply gravity in the opposite direction
//        characterController.Move(gravityDirection * speed * Time.deltaTime);

//        Debug.Log("Character is not grounded" + isGrounded);

//        Debug.Log("Gravity direction when airborne is: " +  gravityDirection);
//    }

//    float horizontalInput = Input.GetAxis("Horizontal"); 
//    float verticalInput = Input.GetAxis("Vertical");

//    // Calculate the move direction in world space
//    Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

//    // Apply speed
//    characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

//    // Apply gravity
//    characterController.Move(gravityDirection * speed * Time.deltaTime); 
//}

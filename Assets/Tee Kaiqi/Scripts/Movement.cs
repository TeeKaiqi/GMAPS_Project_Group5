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
    public LayerMask floorLayer; //the floor layer that will be used in the physics raycast to check if the player is grounded
    public Camera playerCamera; //camera component

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //get charactercontroller component
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, floorLayer); //casts a raycast down from the player's feet and if there's a hit, isGrounded == true

        if (!isGrounded) //if the character is not grounded
        {
            Vector3 gravityDirection = playerCamera.transform.forward; //set the direction of the gravity to be the forward direction of where the camera is looking
            Debug.Log(gravityDirection);

            characterController.Move(gravityDirection * speed * Time.deltaTime); //move the character controller in the gravitydirection
        }
        else //When the character is grounded
        {
            float horizontalInput = Input.GetAxis("Horizontal"); //gets the user input
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; 
            //calculates the move direction by adding the vertical and horizontal input to the gameobject orientation's direction. This makes it so that the movement will be relative to the character in the world space

            characterController.Move(moveDirection.normalized * speed * Time.deltaTime); //move the character by multiplied the normalised move direction, speed and time  
        }
    }

    public void CheckIfJump() //method that checks if the player pressed space to jump
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            float u = Mathf.Sqrt(-2 * Physics.gravity.y * jumpForce); //calculation of the float based on the formula from the jump height physics worksheet 
            Vector3 jumpVelocity = Vector3.up * u; //multiply the up vector by the u force and set that as the jump velocity
            characterController.Move(jumpVelocity * Time.deltaTime); //moves the character 
        }
    }
}
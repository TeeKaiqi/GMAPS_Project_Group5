using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;

    private float speed = 5f; //movement speed 
    private float playerHeight = 1f;
    private float jumpForce = 8f;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public LayerMask Floor;
    bool isGrounded;

    private Vector3 moveDirection;
    private Vector3 gravityDirection; //initial direction of gravity in the beginning and if the character is grounded.

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        CheckIfJump();
    }

    public void HandleMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isGrounded = characterController.isGrounded;

        Debug.Log(characterController.isGrounded);

        //if (characterController.isGrounded)
        //{
        //    gravityDirection = Vector3.down;

        //}
        //else
        //{
        //    gravityDirection = orientation.forward;
        //}

        if (!isGrounded)
        {
            gravityDirection = orientation.forward; // Set gravity direction to player's forward direction when airborne
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

        //Vector3 gravityDirection = orientation.forward;
        //    velocity.y += Physics.gravity.y * Time.deltaTime;
        //    characterController.Move(gravityDirection * velocity.y * Time.deltaTime);
    }

    public void CheckIfJump()
    {
        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
        }
    }
}

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
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public LayerMask GroundLayer;
    bool grounded;

    private Vector3 moveDirection;
    private Vector3 gravityDirection = Vector3.down; //initial direction of gravity in the beginning and if the character is grounded.

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight *0.5f + 0.3f, GroundLayer);
        if (grounded)
        {

        }
    }

    public void HandleMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the move direction in world space
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Apply speed
        characterController.Move(moveDirection.normalized * speed * Time.deltaTime);

        // Apply gravity
        characterController.Move(Vector3.down * speed * Time.deltaTime);
    }

}

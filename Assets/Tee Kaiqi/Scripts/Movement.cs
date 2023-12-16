using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private CharacterController characterController; //get the character controller 
    private float speed = 5f; //movement speed 
    private float rotationSpeed = 5f;
    private Vector3 gravityDirection = Vector3.down; //initial direction of gravity in the beginning and if the character is grounded.

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //get the character controller component
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        MouseRotation();

        if (Input.GetButtonDown("Jump"))
        {

        }
    }

    public void HandleMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        characterController.SimpleMove(movement * speed);
    }

    public void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * rotationSpeed);
        //transform.Rotate(Vector3.left* mouseY * rotationSpeed);
    }

    public void GravityChange()
    {

    }
}

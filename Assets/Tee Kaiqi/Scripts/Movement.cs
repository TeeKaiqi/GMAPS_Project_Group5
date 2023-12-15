using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private CharacterController characterController; //get the character controller 
    private MouseController mouseController;
    private float speed = 5f; //movement speed 
    private Vector3 gravityDirection = Vector3.down; //initial direction of gravity in the beginning and if the character is grounded.

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); //get the character controller component
        mouseController = GetComponent<MouseController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        characterController.Move(movement * Time.deltaTime);

        if (mouseController != null )
        {
            mouseController.RotateCharacter();
        }
    }
}

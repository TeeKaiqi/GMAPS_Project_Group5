using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    private Rigidbody rb;
    public new Camera camera;

    //Set player speed and jump values
    float playerSpeed = 5f;
    float jump = 7.5f;

    bool isGrounded;
    Vector3 camOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Set the default camera offset
        camOffset = new Vector3(0f, 2f, -5f);
    }

    void Update()
    {
        Movement();
        CameraMovement(camOffset);
    }

    //Method to handle player movement
    public void Movement()
    {
        //Moving on X and Z axis
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hInput, 0f, vInput) * playerSpeed * Time.deltaTime;
        transform.Translate(playerMovement);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Use AddForce to simulate jumps
            rb.AddForce(transform.up * jump, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    //Method to handle camera position and rotation
    public void CameraMovement(Vector3 offset)
    {
        //Set the updated camera offset 
        camera.transform.position = transform.position + offset;

        //Set camera rotation to follow player rotation
        camera.transform.rotation = transform.rotation;
    }

    //Handle player death
    void OnTriggerExit(Collider other)
    {
        //Detect if player has fallen out of the main region
        if (other.gameObject.name == "MainRegion")
        {
            //Reset gravity 
            Physics.gravity = new Vector3(0f, -9.81f, 0f);

            //Teleport player back to start position and reset rotation
            transform.position = new Vector3(0f, -8f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);  //Reset rotation

            //Reset camera offset
            camOffset = new Vector3(0f, 2f, -5f);

            Debug.Log("You DIED!");
        }
    }

    //Check collision with region game objects
    void OnTriggerEnter(Collider other)
    {   
        //Shifting gravity upside down if the player is in the upside down region
        if (other.gameObject.name == "UpRegion")
        {
            //Set gravity to go upwards
            Physics.gravity = new Vector3(0f, 9.81f, 0f);

            //Rotate the player upside down
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);

            /*Set a new camera offset value, otherwise the camera 
            would look like it's under the player*/
            camOffset = new Vector3(0f, -2f, -5f);

            Debug.Log("Gravity is now pulling upwards.");
        }

        //Else if statements for other regions because I couldn't find a more efficient way :/
        else if (other.gameObject.name == "LeftRegion")
        {
            //Set the gravity to move to the left
            Physics.gravity = new Vector3(-9.81f, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            camOffset = new Vector3(2f, 0f, -5f);
            Debug.Log("Gravity is now pulling to the left.");
        }

        //Set gravity to move to the right
        else if (other.gameObject.name == "RightRegion")
        {
            //Set the gravity to move to the right
            Physics.gravity = new Vector3(9.81f, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            camOffset = new Vector3(-2f, 0f, -5f);
            Debug.Log("Gravity is now pulling to the right.");
        }

        //Reset everything if player is in the win region
        else if (other.gameObject.name == "WinRegion")
        {
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            camOffset = new Vector3(0f, 2f, -5f);
            Debug.Log("You WON!");
        }
    }
}
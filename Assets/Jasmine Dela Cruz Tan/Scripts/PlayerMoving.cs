using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // Player speed
    public float speed = 4.0f;

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

    //rb and moving of player
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

        // when using 'g' key if press it will call the InvertGravity function and make player can gravity
        if (Input.GetKeyDown(KeyCode.G))
        {
            InvertGravity(); //calls function to start
        }


    }

    private void FixedUpdate()
    {
        //player movement; allows player to move based on the inputs and move the player in the direction of the user inputs
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;  

        //when the gravity is inverted; the movement becomes opposite so this is done to make player still move in the direction of the inputs
        if (GravityInverted)
        {
            //used Quaternion.FromToRotation to create rotation; ref used: https://docs.unity3d.com/ScriptReference/Quaternion.FromToRotation.html#:~:text=Description,direction%20toDirection%20in%20world%20space.
            //ref: used https://gamedev.stackexchange.com/questions/202398/how-does-unitys-fromtorotation-work-a-major-part-of-its-behavior-seems-undocum
            moveDirection = Quaternion.FromToRotation(Vector3.up, -Physics.gravity) * moveDirection;
        }

        //add force to the rb by magnitude and direction(vector) and adds specific speed; moveDirection is a vector that is normalise to change player direction
        rb.AddForce(moveDirection.normalized * speed * 3f, ForceMode.Force);
    }

    void InvertGravity()
    {

        //Use raycast to check the gameobject with specific tags and cast a down vector
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        //Change gravity of player when the raycast hits the game object with the tags and make the player moves according to the gravity 
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Firstgravitychange"))
        {
            Physics.gravity = new Vector3(0f, 0f, -9.8f); //make player right when player presses g ; may not be same for all objects as orientation is all different 
        }

        else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Secondgravitychange")) 
        {
            Physics.gravity = new Vector3(0f, 0f, 9.8f); //make player left when player presses g; may not be same for all objects as orientation is all different 
        }
        else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Thirdgravitychange"))
        {
            Physics.gravity = new Vector3(9.8f, 0f, 0f); //make player right when player presses g ; may not be same for all objects as orientation is all different 
        }
        else // when the ray cast does not hit anything; when player presses g it will make player go up 
        {
            GravityInverted = !GravityInverted; // allow player to change gravity (up dowm) when g is pressed and toggle between gravity to be inverted and not using boolean

            //used the ternary operator to make my code more consise ref: https://www.geeksforgeeks.org/conditional-or-ternary-operator-in-c/
            //if gravity inverted is tru then it will set its gravity to -9.8 (opposite earth gravity) else back to normal  gravoty and make player fall
            Physics.gravity = GravityInverted ? new Vector3(0f, -9.81f, 0f) : new Vector3(0f, 9.81f, 0f); 

            //make raycast longer
            float raycastDistance = 1f;
            Vector3 raycastDirection = GravityInverted ? Vector3.down : Vector3.up; //raycast is casted upwards but if gravity is inverted , ray cast will cast downward

            if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance))
            {
                //to make the player stick to the game object when the gravty is inverted
                float surface = raycastDistance - hit.distance;
                transform.position += raycastDirection * surface;
            }
        }


    }
}

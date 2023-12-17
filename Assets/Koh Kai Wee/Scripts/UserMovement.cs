using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool Rotated;

    public float playerSpeed = 5f;
    public float jump = 7.5f;
    public bool isGrounded;
    public Camera camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Death();
        GravityShift();
        camera.transform.rotation = transform.rotation;
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
            rb.AddForce(transform.up * jump, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    //Method to handle player death
    public void Death()
    {   //If player falls past y=-20, y=20, x=-20, or x=20, they die
        if (transform.position.y < -20 || 20 < transform.position.y || transform.position.x < -20 || 20 < transform.position.x)
        {   //Reset gravity, player position and rotation
            Physics.gravity = new Vector3(0f, -9.8f, 0f);
            transform.position = new Vector3(0f, -8f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Debug.Log("Player died");
        }
    }

    //Method to handle shifting gravity
    public void GravityShift()
    {
        if (transform.position.z > 15 && transform.position.z < 35)
        {   //Set new gravity and player rotation to be upside down
            Physics.gravity = new Vector3(0f, 9.8f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);

            //Set new camera offset
            camera.transform.position = transform.position + new Vector3(0f, -2f, -5f);
        }
        else if (transform.position.z > 35 && transform.position.z < 55)
        {   //Set new gravity and player rotation
            Physics.gravity = new Vector3(-9.8f, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);

            //Set new camera offset
            camera.transform.position = transform.position + new Vector3(2f, 0f, -5f);
        }
        else if (transform.position.z > 55 && transform.position.z < 75)
        {   //Set new gravity and player rotation
            Physics.gravity = new Vector3(9.8f, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            //Set new camera offset
            camera.transform.position = transform.position + new Vector3(-2f, 0f, -5f);
        }
        else if (transform.position.z > 75)
        {   //Reset gravity, player rotation, and camera offset
            Physics.gravity = new Vector3(0f, -9.8f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            camera.transform.position = transform.position + new Vector3(0f, 2f, -5f);
            Debug.Log("You WON :)");
        }
        else
        {
            camera.transform.position = transform.position + new Vector3(0f, 2f, -5f);
        }
    }
}
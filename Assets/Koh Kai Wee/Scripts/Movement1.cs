using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    private Rigidbody rb;

    public float playerSpeed = 5f;
    public float jump = 5f;
    public bool isGrounded;
    //public Camera camera;

    RaycastHit hit;
    LayerMask up;
    LayerMask left;
    LayerMask right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        up = LayerMask.GetMask("Up");
        left = LayerMask.GetMask("Left");
        right = LayerMask.GetMask("Right");
    }

    void Update()
    {
        Movement();
        GravityShift();
        Death();
    }

    public void Movement()  //
    {
        //Moving on X and Z axis
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(hInput, 0f, vInput) * playerSpeed * Time.deltaTime;
        transform.Translate(playerMovement);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    public void Death()
    {   //If player falls past y=-20, y=20, x=-20, or x=20, they die
        if (30 < transform.position.y || transform.position.y < -30 || transform.position.x < -30 || 30 < transform.position.x)
        {   //Teleport back to start point
            transform.position = new Vector3(0f, -8f, 0f);
            Debug.Log("Player died");
        }
    }

    public void GravityShift()
    {
        if (Physics.Raycast(transform.position, transform.up, 50f, up))
        {   //Set gravity to be upside down
            rb.useGravity = false;
            //Use AddForce to simulate upside down gravity
            Physics.gravity = new Vector3(0f, 9.8f, 0f);
            rb.AddForce(new Vector3(0f, 9.8f, 0f), ForceMode.Acceleration);
            Debug.Log("Player is in UP region");
        }
        else if (Physics.Raycast(transform.position, new Vector3(-1f, 0f, 0f), 50f, left))
        {
            rb.useGravity = false;
            rb.AddForce(new Vector3(-9.8f, 0f, 0f), ForceMode.Acceleration);
            Debug.Log("Player is in LEFT region");
        }
        else if (Physics.Raycast(transform.position, new Vector3(1f, 0f, 0f), 50f, right))
        {
            rb.useGravity = false;
            rb.AddForce(new Vector3(9.8f, 0f, 0f), ForceMode.Acceleration);
            Debug.Log("Player is in the RIGHT region");
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
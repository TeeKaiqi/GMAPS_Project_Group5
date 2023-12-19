using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //create sensitivity of the x and y 
    public float sensX;
    public float sensY;

    public Transform orientation; //refernec to the transform orientation of the camera

    float xRotate; //values of the x and y rotation
    float yRotate;

    private void Start()
    {
        //locks and hide the cursor so player can immerse in the game without it blocking
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        //gets the mouse input and change the value of the rotation
        yRotate += mouseX;
        xRotate += mouseY;

        //set camera rotation to limit 
        xRotate = Mathf.Clamp(xRotate, -90f, 90f); 

        //Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        orientation.rotation = Quaternion.Euler(0, yRotate, 0);
    }
}

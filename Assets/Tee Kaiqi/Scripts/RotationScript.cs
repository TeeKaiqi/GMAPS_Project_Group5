using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//rotationscript inspired by https://www.youtube.com/watch?v=f473C43s8nE&ab_channel=Dave%2FGameDevelopment 
//calculations for rotationscript is done separately from movement or camera so that it's cleaner and because I thought i could call it in both camera and movement
public class RotationScript : MonoBehaviour 
{
    public float sensX; //set the x and y sensitivity in the inspector
    public float sensY;
    public float rotationX; //rotation values can be scene in the inspector to check if it works
    public float rotationY; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock the cursor 
        Cursor.visible = false; //make the cursor invisible
    }

    public void Rotate(Transform orientation2)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        //Get the vertical and horizontal mouse movement and multiply by time and sensitivity

        rotationY += mouseX; //update the horizontal and vertical rotation by the mouse movement
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f); //fix the y rotation to limit the angle the player can turn

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); //change the rotation of the camera based on the rotationX and rotationY
        orientation2.rotation = Quaternion.Euler(0, rotationY, 0); //change the rotation of the orientation to follow the horizontal orientation
    }

    //originally tried to use this function to get the forward vector of the orientation gameobject of the player but didn't end up using it 
    //public Vector3 ForwardDirection(Transform orientation) 
    //{
    //    Vector3 forwardDirection = orientation.forward;
    //    forwardDirection = Quaternion.Euler(0, rotationY, 0) * forwardDirection;
    //    forwardDirection = Quaternion.Euler(rotationX, 0, 0) * forwardDirection;
    //    Debug.Log("Forward Direction: " + forwardDirection);
    //    return forwardDirection;
    //}
}

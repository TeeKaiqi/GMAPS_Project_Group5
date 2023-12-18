using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float sensX;
    public float sensY;
    float rotationX;
    float rotationY; //set the game object in inspector that holds the orientation of where the player is facing



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Rotate(Transform orientation)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    public Vector3 ForwardDirection(Transform orientation)
    {
        Vector3 forwardDirection = orientation.forward;
        Debug.Log("Forward Direction: " + forwardDirection);
        return forwardDirection;
    }
}

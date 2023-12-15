using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour //script that has the mouse movement. Script will handle the rotation of the camera/player as well as changing of the gravity point
{
    public float rotationSpeed = 3f;
    public Vector2 turn;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks the cursor to the centre of the screen
        Cursor.visible = false; //makes the cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
    }

    public void RotateCharacter()
    {
        turn.x = Input.GetAxis("Mouse X");
        turn.y = Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}

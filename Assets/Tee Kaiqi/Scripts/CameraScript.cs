using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform orientation;
    private RotationScript rotation;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rotation = GetComponent<RotationScript>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

// Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        rotation.Rotate(orientation);
    }
}
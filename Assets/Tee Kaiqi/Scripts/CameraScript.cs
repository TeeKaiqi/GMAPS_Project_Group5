using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform orientation; //takes the orientation game object 
    private RotationScript rotation; //references rotationscript 
    public Transform target; //set the character as the target in the inspector

    // Start is called before the first frame update
    void Start()
    {
        rotation = GetComponent<RotationScript>(); //get the rotation script
    }

// Update is called once per frame
    void Update()
    {
        transform.position = target.position; //change the position of the camera to the position of the target (character)
        rotation.Rotate(orientation); //rotate the camera accordingly by calling the rotate function from the rotationscript and passing in the orientation of the player
    }
}

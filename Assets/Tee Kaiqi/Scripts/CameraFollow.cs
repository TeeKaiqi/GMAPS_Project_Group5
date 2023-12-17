using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraPosition;
    public Vector3 offset = new Vector3(0, 2f, -6f); //the coordinates that the camera should be offset by so that the camera is slightly behind the character

    private void Update()
    {
        transform.position = cameraPosition.position + offset;
    }
    //public Transform target; //public target that has to be set in inspector
    //public Vector3 offset = new Vector3 (0, 2f, -6f); //the coordinates that the camera should be offset by so that the camera is slightly behind the character

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    FollowTarget(); //calls the followtarget function
    //}

    //public void FollowTarget()
    //{
    //    transform.position = target.position + offset; //adds the offset vector to the position of character that is the target
    //    transform.LookAt(target); //look at the target that has the offset added
    //}

}

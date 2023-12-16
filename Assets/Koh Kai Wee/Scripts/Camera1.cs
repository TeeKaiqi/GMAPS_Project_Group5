using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraPos = new Vector3(0f, 2f, -5f);
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = player.position + cameraPos;
    }
}

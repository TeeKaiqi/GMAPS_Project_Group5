using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float playerSpeed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(hInput, 0f, vInput) * playerSpeed * Time.deltaTime;
        transform.Translate(playerMovement);
    }
}
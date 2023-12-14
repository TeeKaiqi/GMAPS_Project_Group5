using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction, this doesnt take into account vertical movement
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Move the object
        transform.Translate(movement * speed * Time.deltaTime);
    }
}

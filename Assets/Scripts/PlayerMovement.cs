using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float movementSpeed = 12f;

    // Update is called once per frame
    void Update()
    {
        // Get the input from the arrow keys
        float rightDirection = Input.GetAxis("Horizontal");
        float forwardDirection = Input.GetAxis("Vertical");

        // Direction that we want to move in based on the input
        // Takes into account the local coordinates of the player,
        // i.e. the direction we are facing
        Vector3 movement = transform.right * rightDirection
            + transform.forward * forwardDirection;

        Debug.Log(movement);

        controller.SimpleMove(movement * movementSpeed);
    }
}

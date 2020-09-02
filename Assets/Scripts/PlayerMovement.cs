using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    /** PUBLIC VARIABLES */
    public CharacterController controller;
    public float movementSpeed = 12f;
    public float radiusToInteract = 2f;

    public string prompt;

    public Text promptText;

    /** PRIVATE VARIABLES */
    private Camera playerCamera;
    private InteractableObject focus;

    private void Start()
    {
        playerCamera = Camera.main;
        SetPromptText("");
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        findInteractable();

        // Detect key presses
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactWithFocus();
        }
    }

    private void interactWithFocus()
    {
        if (focus != null)
        {
            Debug.Log("Interacting with object " + focus.name);
            focus.Interact();
        }
    }

    private void findInteractable()
    {
        // Check if we are looking at an interactable object
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, radiusToInteract))
        {
            InteractableObject interactable =
                hit.collider.GetComponent<InteractableObject>();

            // If we hit an interactable, display the prompt
            if (interactable != null)
            {
                focus = interactable;
                SetPromptText("Press E to interact");
            }
            else
            {
                SetPromptText("");
            }
        }
        else
        {
            SetPromptText("");
        }
    }

    private void movePlayer()
    {
        // Get the input from the arrow keys
        float rightDirection = Input.GetAxis("Horizontal");
        float forwardDirection = Input.GetAxis("Vertical");

        // Direction that we want to move in based on the input
        // Takes into account the local coordinates of the player,
        // i.e. the direction we are facing
        Vector3 movement = transform.right * rightDirection
            + transform.forward * forwardDirection;

        controller.SimpleMove(movement * movementSpeed);
    }

    private void SetPromptText(string prompt)
    {
        promptText.text = prompt;
    }
}

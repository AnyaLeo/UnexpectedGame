using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class PlayerMovement : MonoBehaviour
{
    /** PUBLIC VARIABLES */
    public CharacterController controller;
    public float movementSpeed = 12f;
    public float radiusToInteract = 4f;

    public Image crosshair;

    public string prompt;

    public Text promptText;

    public AudioSource walkingAudioClip;

    public UIManager dialogueManager;

    public GameMode gameMode;

    public float crouchOffset = 0.5f;
    public float crouchTime = 0.5f;

    /** PRIVATE VARIABLES */
    private Camera playerCamera;
    private InteractableObject focus;

    // Crouching/Uncrouching related variables
    private bool bIsCrouching;
    private float currentVelocity = 0.0f;
    private float cameraHeight;

    private void Start()
    {
        playerCamera = Camera.main;
        SetPromptText("");

        bIsCrouching = false;
        cameraHeight = playerCamera.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement is disallowed when we are talking to something
        if (!VD.isActive)
        {
            crosshair.enabled = true;
            movePlayer();
        }
        else
        {
            crosshair.enabled = false;
            walkingAudioClip.Stop();
        }

        // Deal with interactables
        // The current interactable will be stored in a variable "focus"

        findInteractable();
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactWithFocus();
        }

        // To continue dialogue, press return
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.CallNextNode();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            setCrouch();
        }

        if (bIsCrouching)
        {
            smoothCrouching(-crouchOffset);
        } 
        else
        {
            smoothCrouching(cameraHeight);
        }
    }

    // Used this:
    // https://answers.unity.com/questions/482882/how-can-i-smooth-out-the-crouch-movement.html
    // to develop a smooth crouching 
    private void smoothCrouching(float targetYPos)
    {
        float newY = Mathf.SmoothDamp(
                playerCamera.transform.localPosition.y,
                targetYPos,
                ref currentVelocity,
                crouchTime);
        playerCamera.transform.localPosition = new Vector3(0, newY, 0);
    }

    private void setCrouch()
    {
        bIsCrouching = !bIsCrouching;
        currentVelocity = 0.0f;
    }

    private void interactWithFocus()
    {
        if (focus != null)
        {
            if (!VD.isActive)
            {
                VIDE_Assign dialogue = focus.GetComponent<VIDE_Assign>();
                dialogueManager.StartDialogue(dialogue);
            }

            focus.Interact();

            // TODO: this would work MUCH better if Interact was called on the
            // InteractableObject, consider how we can move
            // starting the dialogue to the interactable object
            PickupObject pickup = focus.GetComponent<PickupObject>();
            if (pickup != null)
            {
                // This is checking if we picked up the cards in Chapter 1
                if (pickup.canBePickedUp)
                {
                    gameMode.changeKeyAnimation();
                }
            }
        }
    }

    private void findInteractable()
    {
        // Check if we are looking at an interactable object
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 500, Color.red);
        if (Physics.Raycast(ray, out hit, radiusToInteract))
        {
            InteractableObject interactable =
                hit.collider.GetComponent<InteractableObject>();

            

            // If we hit an interactable, display the prompt
            // But only if we are not in the middle of a dialogue
            if (interactable != null && !VD.isActive)
            {
                focus = interactable;
                SetPromptText("Press E to interact");
            }
            else
            {
                focus = null;
                SetPromptText("");
            }
        }
        else
        {
            focus = null;
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

        bool shouldPlayWalkingSound = 
            !(Mathf.Approximately(rightDirection, 0f)
            && Mathf.Approximately(forwardDirection, 0f));

        setWalkingSound(shouldPlayWalkingSound);
    }

    private void setWalkingSound(bool shouldPlay)
    {
        if (shouldPlay && !walkingAudioClip.isPlaying)
        {
            walkingAudioClip.Play();
        }
        else if (!shouldPlay)
        {
            walkingAudioClip.Stop();
        }
    }

    private void SetPromptText(string prompt)
    {
        promptText.text = prompt;
    }
}

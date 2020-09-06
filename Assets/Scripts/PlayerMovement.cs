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

    public string prompt;

    public Text promptText;

    public AudioSource walkingAudioClip;

    public UIManager dialogueManager;

    public GameMode gameMode;

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
        // Movement is disallowed when we are talking to something
        if (!VD.isActive)
        {
            movePlayer();
        }
        else
        {
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

            // TODO: this would work MUCH better if Interact was called on the
            // InteractableObject, consider how we can move
            // starting the dialogue to the interactable object
            PickupObject pickup = focus.GetComponent<PickupObject>();
            if (pickup != null)
            {
                pickup.Interact();

                // TODO: dont bother the game mode from the player controller
                // like seriously
                if (pickup.canBePickedUp)
                {
                    gameMode.isCardGameInProgress = true;
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

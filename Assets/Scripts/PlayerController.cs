using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the behaviour of the player
// Movement is handled separately in PlayerMovement
public class PlayerController : MonoBehaviour
{
    Camera playerCamera;
    float radiusToInteract = 3;

    private InteractableObject focus;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we hit an interactable object with a ray cast
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, radiusToInteract))
        {
            InteractableObject interactable =
                hit.collider.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                SetFocus(interactable);
            }
        }
    }

    void SetFocus (InteractableObject newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                RemoveFocus();
                focus = newFocus;
            }
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus ()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
    }
}

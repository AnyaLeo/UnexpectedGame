using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public PickupObject pickupRelyingOnUs;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        DisplayText();
    }

    private void DisplayText()
    {
        Debug.Log("Interacting");
    }

    // Helper function to visualize the interaction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    public void allowPickupToBePickedUp()
    {
        // This is a really bad way to do things isn't it
        if (pickupRelyingOnUs != null)
        {
            pickupRelyingOnUs.canBePickedUp = true;
        }
    }
}

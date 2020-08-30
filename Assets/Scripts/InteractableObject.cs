using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionRadius = 3f;

    public Canvas canvas;

    public string ObjectName = "Object";
    public string Dialogue = "Dialogue";

    private bool isFocus = false;
    private Transform player;
    private bool hasInteracted = false;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
    }

    private void DisplayText()
    {
        Debug.Log(ObjectName + "\n" + Dialogue);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= interactionRadius)
            {
                DisplayText();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        //hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
        Debug.Log("set to false");
    }

    // Helper function to visualize the interaction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

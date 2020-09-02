using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ObjectName = "Object";
    public string Dialogue = "Dialogue";

    public GameObject promptPrefab; 

    //private Transform player;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        DisplayText();
    }

    private void DisplayText()
    {
        Debug.Log(ObjectName + ": " + Dialogue);
    }

    // Helper function to visualize the interaction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

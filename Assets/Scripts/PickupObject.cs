using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PickupObject is one per chapter
// When the object is picked up, the game changes in some way
// (usually allows us to start the card game)

public class PickupObject : InteractableObject
{
    public bool canBePickedUp { get; set; }

    public void Start()
    {
        canBePickedUp = false;
    }

    public override void Interact()
    {
        if (canBePickedUp)
        {
            gameObject.SetActive(false);
        }
    }
}

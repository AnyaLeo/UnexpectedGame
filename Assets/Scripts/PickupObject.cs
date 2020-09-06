using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PickupObject is one per chapter
// When the object is picked up, the game changes in some way
// (usually allows us to start the card game)

public class PickupObject : InteractableObject
{
    public override void Interact()
    {
        gameObject.SetActive(false);
    }
}

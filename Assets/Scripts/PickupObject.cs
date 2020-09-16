using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PickupObject is one per chapter
// When the object is picked up, the game changes in some way
// (usually allows us to start the card game)

public class PickupObject : InteractableObject
{
    // Quick fix [1] for daemon's portrait disappearing
    // Because the dialogue for the pickup cycles through the 
    // changing of the key animation
    public GameMode gameMode;
    public bool isThirdChapterPickup; 

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

            // Quick fix [1]
            if (isThirdChapterPickup)
            {
                gameMode.changeKeyAnimation();
            }
        }

    }
}

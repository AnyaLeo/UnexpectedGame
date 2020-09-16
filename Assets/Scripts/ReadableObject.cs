using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Readable Object will spawn a letter with the text specified by the user.

public class ReadableObject : InteractableObject
{
    [TextArea]
    public string letterContent;
    public GameObject letterPrefab;

    public Canvas canvas;

    private GameObject letterSpawned;
    public bool isPlayerInteracting { get; set; }

    public bool Ch3_stoppedInteracting;

    public bool bAlreadyRead { get; set; }

    private void Start()
    {
        bAlreadyRead = false;
        Ch3_stoppedInteracting = false;
    }

    public override void Interact()
    {
        if (letterPrefab != null)
        {
            letterSpawned = Instantiate(letterPrefab) as GameObject;

            Vector3 letterPosition = new Vector3(0f, 0f, 0f);

            letterSpawned.transform.SetParent(canvas.transform, false);
            letterSpawned.transform.localPosition = letterPosition;
            letterSpawned.transform.SetSiblingIndex(3);

            // Set the text content of the letter
            Text letterText = letterSpawned.GetComponentInChildren<Text>();
            if (letterText != null)
            {
                letterText.text = letterContent; 
            } 
            else
            {
                Debug.Log("ReadableObject: Does not have component Text.");
            }
            isPlayerInteracting = true;
        }
        else
        {
            Debug.Log("ReadableObject: tried to spawn a letter without letter prefab specified");
        }

        // Ch3: Quick and dirty fix to allow us to pick up ace of spades 
        // after reading the textbook
        if(pickupRelyingOnUs != null)
        {
            pickupRelyingOnUs.canBePickedUp = true;
        }
    }

    private void StopInteracting()
    {
        if (isPlayerInteracting)
        {
            Destroy(letterSpawned);
            isPlayerInteracting = false;
            Ch3_stoppedInteracting = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInteracting && Input.GetKeyDown(KeyCode.Space))
        {
            StopInteracting();
        }
    }
}

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

    public override void Interact()
    {
        if (letterPrefab != null)
        {
            GameObject letterSpawned = Instantiate(letterPrefab) as GameObject;

            Vector3 letterPosition = new Vector3(0f, 0f, 0f);

            letterSpawned.transform.SetParent(canvas.transform);
            letterSpawned.transform.localPosition = letterPosition;

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
        }
        else
        {
            Debug.Log("ReadableObject: tried to spawn a letter without letter prefab specified");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Readable Object will spawn a letter with the text specified by the user.

public class ReadableObject : InteractableObject
{
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

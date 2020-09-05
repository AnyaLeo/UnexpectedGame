using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class GameMode : MonoBehaviour
{
    public UIManager dialogueManager;

    private bool hasDialoguePlayed;

    // Start is called before the first frame update
    void Start()
    {
        hasDialoguePlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDialoguePlayed)
        {
            hasDialoguePlayed = true;
            VIDE_Assign startDialogue = this.GetComponent<VIDE_Assign>();
            dialogueManager.StartDialogue(startDialogue);
        }
    }
}

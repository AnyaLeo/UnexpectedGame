using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDaemon : MonoBehaviour
{
    public VIDE_Assign dialogue;
    public string newDialogue;

    public void changeDialogue()
    {
        dialogue.AssignNew(newDialogue);
    }
}

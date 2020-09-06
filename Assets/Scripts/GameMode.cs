using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class GameMode : MonoBehaviour
{
    public UIManager dialogueManager;

    // TODO: consider making this more extendable
    public GameObject pictureDisplayed1;
    public GameObject pictureDisplayed2;


    private bool hasDialoguePlayed;
    public bool isCardGameInProgress { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        hasDialoguePlayed = false;
        isCardGameInProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Consider moving it somewhere that is called in the beginning of the game
        if (!hasDialoguePlayed)
        {
            hasDialoguePlayed = true;
            VIDE_Assign startDialogue = this.GetComponent<VIDE_Assign>();
            dialogueManager.StartDialogue(startDialogue);
        }

        // TODO: DO THIS BETTER after demo
        if (isCardGameInProgress)
        {
            pictureDisplayed1.SetActive(false);
            pictureDisplayed2.SetActive(true);

            isCardGameInProgress = false;
        }
    }
}

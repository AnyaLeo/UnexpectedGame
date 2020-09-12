using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class GameMode : MonoBehaviour
{
    public UIManager dialogueManager;

    public GameObject[] keyPictureAnimations;
    private int keyAnimationIndex;

    private bool hasDialoguePlayed;
    public bool isCardGameInProgress { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        keyAnimationIndex = 0;
        hasDialoguePlayed = false;

        // TODO: clean up since the variable is no longer in use
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
    }

    // Display the next animation in the queue
    public void changeKeyAnimation()
    {
        keyPictureAnimations[keyAnimationIndex].SetActive(false);
        keyAnimationIndex++;
        keyPictureAnimations[keyAnimationIndex].SetActive(true);
    }
}

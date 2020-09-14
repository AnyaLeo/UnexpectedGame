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

    // VIDE editor does not allow me to access functions in ace of spades
    // so we'll have to show it manually for Chapter 1
    public GameObject aceOfSpades;

    public AudioSource ch1_music;
    public AudioSource ch2_music;
    public AudioSource ch3_music;

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

    public void showAceOfSpades()
    {
        aceOfSpades.SetActive(true);
    }

    public void setCh1Music(bool shouldPlay)
    {
        playLoopedMusic(ch1_music, shouldPlay);
    }

    public void setCh2Music(bool shouldPlay)
    {
        playLoopedMusic(ch2_music, shouldPlay);
    }

    public void setCh3Music(bool shouldPlay)
    {
        playLoopedMusic(ch3_music, shouldPlay);
    }

    private void playLoopedMusic(AudioSource music, bool shouldPlay)
    {
        if (shouldPlay)
        {
            music.loop = true;
            music.Play();
        }
        else
        {
            music.Stop();
        }
    }

}

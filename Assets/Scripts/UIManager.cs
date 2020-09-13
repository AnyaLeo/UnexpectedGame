using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VIDE_Data; // to retrieve node data

public class UIManager : MonoBehaviour
{
    public GameObject dialogueContainer;
    public GameObject containerNPC;
    public GameObject containerPlayer;

    //public Color neutralTextColor;
    //public Color selectedTextColor = Color.yellow;

    public Text textNPC;
    public Text[] textPlayer;
    public Text labelNPC;

    // UI for the pages found in chapter 2
    public GameObject Ch2_pagesFound;

    private int selectedChoiceIndex;
    private int playerChoiceCount;

    // Start is called before the first frame update
    void Start()
    {
        DisableContainers();
        playerChoiceCount = 0; 

        if (Ch2_pagesFound != null)
        {
            Ch2_pagesFound.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var data = VD.nodeData;

        if (VD.isActive)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (data.commentIndex < data.comments.Length - 1)
                {
                    textPlayer[selectedChoiceIndex].color = Color.white;

                    data.commentIndex++;
                    selectedChoiceIndex++;

                    textPlayer[selectedChoiceIndex].color = Color.yellow;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (data.commentIndex > 0)
                {
                    textPlayer[selectedChoiceIndex].color = Color.white;

                    data.commentIndex--;
                    selectedChoiceIndex--;

                    textPlayer[selectedChoiceIndex].color = Color.yellow;
                }
            }
        }
    }

    public void StartDialogue(VIDE_Assign dialogue)
    {
        if (dialogue != null)
        {
            VD.OnNodeChange += UpdateUI;
            VD.OnEnd += End;
            VD.BeginDialogue(dialogue);
        }
        else
        {
            Debug.Log("UIManager, StartDialogueWith: tried to start a dialogue without proper dialogue specified");
        }
    }

    void Begin()
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }

    void UpdateUI(VD.NodeData data)
    {
        DisableContainers();

        dialogueContainer.SetActive(true);
        if (data.isPlayer)
        {
            containerPlayer.SetActive(true);
            playerChoiceCount = 0;

            for (int i = 0; i < textPlayer.Length; i++)
            {
                // Decide whether we want to show the choice button or no
                if (i < data.comments.Length)
                {
                    textPlayer[i].enabled = true;
                    textPlayer[i].text = data.comments[i];
                    textPlayer[i].color = Color.white;
                    playerChoiceCount++;
                } 
                else
                {
                   textPlayer[i].enabled = false;
                }
            }

            // Select the first choice as the default
            selectedChoiceIndex = 0;
            textPlayer[selectedChoiceIndex].color = Color.yellow;
        }
        else
        {
            // Get the data from the node for the NPC text
            containerNPC.SetActive(true);
            textNPC.text = data.comments[data.commentIndex];
        }
    }

    void End(VD.NodeData data)
    {
        DisableContainers();
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    void DisableContainers()
    {
        containerNPC.SetActive(false);
        containerPlayer.SetActive(false);
        dialogueContainer.SetActive(false);
    }

    private void OnDisable()
    {
        if (containerNPC != null)
        {
            End(null);
        }
    }

    // buttons will call this so we get all the choices for the player node

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        // Release the mouse button
        if (Input.GetMouseButtonUp(0))
        {
            VD.Next();
        }
    }

    public void SetNPCLabel(string newLabel)
    {
        if (labelNPC != null)
        {
            labelNPC.text = newLabel;
        }
    }

    public void SetPagesFoundUI(bool isActive)
    {
        if (Ch2_pagesFound != null)
        {
            Debug.Log("Setting pages to active: " + isActive);
            Ch2_pagesFound.SetActive(isActive);
        }
    }

    public void SetNumberOfPagesFound(int number)
    {
        if (Ch2_pagesFound != null)
        {
            TextMeshProUGUI textComp = Ch2_pagesFound.GetComponent<TextMeshProUGUI>();

            if(textComp != null)
            {
                textComp.text = "Pages found: " + number + "/5";
            }
        }
    }
} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data; // to retrieve node data

public class UIManager : MonoBehaviour
{
    public GameObject containerNPC;
    public GameObject containerPlayer;
    public Text textNPC;
    public Text[] textPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DisableContainers();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: move the activation of the dialogue to the PlayerController
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!VD.isActive)
            {
                Begin();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (VD.isActive)
            {
                VD.Next();
            }
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

        if (data.isPlayer)
        {
            containerPlayer.SetActive(true);

            for (int i = 0; i < textPlayer.Length; i++)
            {
                // Decide whether we want to show the choice button or no
                if (i < data.comments.Length)
                {
                    textPlayer[i].transform.parent.gameObject.SetActive(true);
                    textPlayer[i].text = data.comments[i];
                } 
                else
                {
                    textPlayer[i].transform.parent.gameObject.SetActive(false);
                }
            }

            // Select the first choice as the default
            textPlayer[0].transform.parent.GetComponent<Button>().Select();
            textPlayer[0].transform.parent.GetComponent<Button>().OnSelect(null);
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
}

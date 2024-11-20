using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CatDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogueLines; // Dialogue lines for the NPC
    [SerializeField] private Animator endScreenAnimator;
    private int index;

    public float wordSpeed;
    private bool isPlayerNearby = false;
    [SerializeField] private GameObject pressPrompt;    // UI prompt to show when near NPC

    void Start()
    {
        if (pressPrompt != null)
        {
            pressPrompt.SetActive(false); // Hide the prompt initially
        }
        resetPanel();
    }

    void Update()
    {
        // Check for interaction input
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z)) 
        {
            if (dialoguePanel.activeInHierarchy)
            {
                // If typing is complete, go to the next line
                if (dialogueText.text == dialogueLines[index])
                {
                    NextLine();
                }
                else
                {
                    // Finish typing the current line immediately
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[index];
                }
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }


    public void resetPanel()
    {
        index = 0;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if(index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            EndDialogueSequence();
        }
    }

    private void EndDialogueSequence()
    {
        resetPanel();

        if (endScreenAnimator != null)
        {
            endScreenAnimator.SetTrigger("PlayEnding"); // Trigger "TheEndScreen" animation
        }

        StartCoroutine(ExitToEndMenu()); // Start transition to the EndMenu scene
    }

    IEnumerator ExitToEndMenu()
    {
        yield return new WaitForSeconds(16f); // Wait for the animation to finish
        SceneManager.LoadScene("EndMenu"); // Load the EndMenu scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (pressPrompt != null) pressPrompt.SetActive(true); // Show interaction prompt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pressPrompt != null) pressPrompt.SetActive(false); // Hide interaction prompt
            resetPanel();
        }
    }
}

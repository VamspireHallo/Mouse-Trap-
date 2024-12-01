using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMemory : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel; // The dialogue UI panel
    [SerializeField] private TextMeshProUGUI dialogueText;       // The text UI for displaying dialogue
    [SerializeField] private string[] dialogueLines;   // The array of dialogue lines
    [SerializeField] private KeyCode interactKey = KeyCode.Z; // Key to interact (default: Z)
    [SerializeField] private GameObject PlayerUI;

    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;

    private void Start()
    {
        // Ensure the dialogue panel is inactive at the start
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player is nearby and presses the interact key
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        PlayerUI.SetActive(false);
        if (dialogueLines.Length > 0)
        {
            isDialogueActive = true;
            currentLineIndex = 0;
            dialoguePanel.SetActive(true);
            UpdateDialogueText();
        }
    }

    private void AdvanceDialogue()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            UpdateDialogueText();
        }
        else
        {
            EndDialogue();
        }
    }

    private void UpdateDialogueText()
    {
        if (dialogueText != null)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        PlayerUI.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMemory : MonoBehaviour
{
    [SerializeField] private GameObject memoryPanel; // The dialogue UI panel
    [SerializeField] private TextMeshProUGUI memoryText; // The text UI for displaying dialogue
    [SerializeField] private string[] memoryLines; // The array of dialogue lines
    [SerializeField] private KeyCode interactKey = KeyCode.Z; // Key to interact (default: Z)
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private float wordSpeed = 0.05f; // Speed of the typing effect

    private int index;
    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;
    private bool isMemoryActive = false;
    private bool isTyping = false; // Track if the text is currently being typed
    private bool hasDialogueCompleted = false;

    void Start()
    {
        resetPanel();
        if (memoryPanel != null) memoryPanel.SetActive(false);
        if (PlayerUI != null) PlayerUI.SetActive(true);
    }

    void Update()
    {
        // Check for interaction input
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z) && !hasDialogueCompleted) 
        {
            if (memoryPanel.activeInHierarchy)
            {
                // If typing is complete, go to the next line
                if (memoryText.text == memoryLines[index])
                {
                    NextLine();
                }
                else
                {
                    // Finish typing the current line immediately
                    StopAllCoroutines();
                    memoryText.text = memoryLines[index];
                }
            }
            else
            {
                PlayerUI.SetActive(false);
                memoryPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }


    public void resetPanel()
    {
        index = 0;
        memoryText.text = "";
        memoryPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in memoryLines[index].ToCharArray())
        {
            memoryText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if(index < memoryLines.Length - 1)
        {
            index++;
            memoryText.text = "";
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
        hasDialogueCompleted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialogueCompleted)
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            resetPanel();
        }
    }
}

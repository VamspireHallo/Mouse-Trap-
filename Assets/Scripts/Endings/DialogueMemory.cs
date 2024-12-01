using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMemory : MonoBehaviour
{
    [SerializeField] private GameObject memoryPanel; // The dialogue UI panel
    [SerializeField] private TextMeshProUGUI memoryText;       // The text UI for displaying dialogue
    [SerializeField] private string[] memoryLines;   // The array of dialogue lines
    [SerializeField] private KeyCode interactKey = KeyCode.Z; // Key to interact (default: Z)
    [SerializeField] private GameObject PlayerUI;

    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;
    private bool isMemoryActive = false;

    private void Start()
    {
        // Ensure the memory panel is inactive at the start
        if (memoryPanel != null)
        {
            memoryPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // If the memory is active and the player presses the interact key, transition the dialogue
        if (isMemoryActive && Input.GetKeyDown(interactKey))
        {
            AdvanceMemory();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (!isMemoryActive)
            {
                StartMemory();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            EndMemory();
        }
    }

    private void StartMemory()
    {
        if (memoryLines.Length > 0)
        {
            isMemoryActive = true;
            currentLineIndex = 0;
            memoryPanel.SetActive(true);
            UpdateMemoryText();
        }
    }

    private void AdvanceMemory()
    {
        currentLineIndex++;
        if (currentLineIndex < memoryLines.Length)
        {
            UpdateMemoryText();
        }
        else
        {
            EndMemory();
        }
    }

    private void UpdateMemoryText()
    {
        if (memoryText != null)
        {
            memoryText.text = memoryLines[currentLineIndex];
        }
    }

    private void EndMemory()
    {
        isMemoryActive = false;
        if (memoryPanel != null)
        {
            memoryPanel.SetActive(false);
        }
    }
}

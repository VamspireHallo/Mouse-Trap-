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
    [SerializeField] private float typingSpeed = 0.05f; // Speed of the typing effect

    private bool isPlayerNearby = false;
    private int currentLineIndex = 0;
    private bool isMemoryActive = false;
    private bool isTyping = false; // Track if the text is currently being typed
    private Coroutine typingCoroutine;

    private void Start()
    {
        // Ensure the memory panel and PlayerUI are inactive at the start
        if (memoryPanel != null) memoryPanel.SetActive(false);

        if (PlayerUI != null) PlayerUI.SetActive(true); // Ensure PlayerUI is active initially
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

            if (PlayerUI != null) PlayerUI.SetActive(false); // Hide PlayerUI
            if (memoryPanel != null) memoryPanel.SetActive(true); // Show memory dialogue

            DisplayCurrentLine();
        }
    }

    private void AdvanceMemory()
    {
        if (typingCoroutine != null)
        {
            // If the text is still typing, stop the coroutine and display the full line immediately
            StopCoroutine(typingCoroutine);
            memoryText.text = memoryLines[currentLineIndex];
            isTyping = false;
            return;
        }

        currentLineIndex++;
        if (currentLineIndex < memoryLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndMemory();
        }
    }

    private void DisplayCurrentLine()
    {
        if (memoryText != null)
        {
            memoryText.text = ""; // Clear the text
            typingCoroutine = StartCoroutine(Typing(memoryLines[currentLineIndex]));
        }
    }

    private IEnumerator Typing(string line)
    {
        isTyping = true;
        foreach (char letter in line.ToCharArray())
        {
            memoryText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void EndMemory()
    {
        isMemoryActive = false;

        if (memoryPanel != null) memoryPanel.SetActive(false); // Hide memory dialogue
        if (PlayerUI != null) PlayerUI.SetActive(true); // Show PlayerUI
    }
}

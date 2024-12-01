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
    [SerializeField] private GameObject PlayerUI;


    private int index;
    private bool isPlayerNearby = false;
    private bool isMemoryActive = false;

    void Start()
    {
        ResetPanel();
    }

    void Update()
    {
        // Allow the player to press Z to advance the dialogue
        if (isMemoryActive && Input.anyKeyDown)
        {
            NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isMemoryActive)
        {
            isPlayerNearby = true;
            StartMemory();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            ResetPanel();
        }
    }

    private void StartMemory()
    {
        if (memoryLines.Length > 0)
        {
            isMemoryActive = true;
            index = 0;
            memoryPanel.SetActive(true);
            PlayerUI.SetActive(false); // Hide PlayerUI during the dialogue
            DisplayCurrentLine();
        }
    }

    private void DisplayCurrentLine()
    {
        if (memoryText != null)
        {
            memoryText.text = memoryLines[index]; // Display the full line immediately
        }
    }

    public void NextLine()
    {
        if (index < memoryLines.Length - 1)
        {
            index++;
            DisplayCurrentLine();
        }
        else
        {
            EndMemory();
        }
    }

    private void EndMemory()
    {
        isMemoryActive = false;
        ResetPanel();
    }

    private void ResetPanel()
    {
        index = 0;
        memoryText.text = "";
        memoryPanel.SetActive(false);
        PlayerUI.SetActive(true); // Restore PlayerUI when the dialogue ends
    }
}

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
    [SerializeField] private PlayerController playerController; // Reference to the player's movement script
    [SerializeField] private Animator playerAnimator; // Reference to the player's animator

    private int index;
    private bool isMemoryActive = false;
    public bool dialogueDone = false;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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
            StartMemory();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
            if (playerController != null) playerController.enabled = false;

            if (playerAnimator != null)
            {
                // Set to idle
                playerAnimator.SetBool("isMoving", false); 
                playerAnimator.SetBool("isFalling", false);
                playerAnimator.SetBool("isJumping", false);
            }
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
        if (playerController != null) playerController.enabled = true;
        dialogueDone = true;
    }

    private void ResetPanel()
    {
        index = 0;
        memoryText.text = "";
        memoryPanel.SetActive(false);
        PlayerUI.SetActive(true); // Restore PlayerUI when the dialogue ends
    }
}

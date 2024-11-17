using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private List<string> dialogueLines; // Dialogue lines for the NPC
    [SerializeField] private GameObject pressPrompt;    // UI prompt to show when near NPC

    private bool isPlayerNearby = false;

    void Start()
    {
        if (pressPrompt != null)
        {
            pressPrompt.SetActive(false); // Hide the prompt initially
        }
    }

    void Update()
    {
        // Check for interaction input
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z)) 
        {
            DialogueManager.Instance.StartDialogue(dialogueLines); // Start dialogue
        }
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
        }
    }
}

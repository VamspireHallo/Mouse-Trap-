using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    [SerializeField] public GameObject noteUI; // Reference to the UI Canvas or Panel for the note
    [SerializeField] public GameObject pressPrompt;
    private bool isPlayerNearby = false;
    private bool isNoteOpen = false;
    
    private SpriteRenderer spriteRenderer; // Reference to the note's sprite renderer
    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        // Initially, the note UI should be hidden
        noteUI.SetActive(false);
        pressPrompt.gameObject.SetActive(false);

        // Get the SpriteRenderer component to change color later
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor; // Set to the default color at start
    }

    void Update()
    {
        // Check if player presses "Z" and is near the note
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isNoteOpen)
            {
                OpenNote();
            }
            else
            {
                CloseNote();
            }
        }
    }

    // When the player enters the trigger area (near the note object)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            spriteRenderer.color = glowColor; // Change to the glowing color
            pressPrompt.gameObject.SetActive(true);
        }
    }

    // When the player exits the trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            spriteRenderer.color = normalColor; // Revert to the normal color
            pressPrompt.gameObject.SetActive(false);
        }
    }

    // Function to open the note
    private void OpenNote()
    {
        pressPrompt.gameObject.SetActive(false); // Remove prompt from screen
        noteUI.SetActive(true); // Show the note UI
        isNoteOpen = true;
        // Optional: Pause the game or movement while the note is open
        Time.timeScale = 0f; // This will pause the game
    }

    // Function to close the note
    private void CloseNote()
    {
        noteUI.SetActive(false); // Hide the note UI
        isNoteOpen = false;
        // Optional: Resume the game or movement if you paused it
        Time.timeScale = 1f; // This will resume the game
    }
}

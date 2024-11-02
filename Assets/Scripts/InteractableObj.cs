using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] public GameObject objUI; // Reference to the UI Canvas or Panel for the obj
    [SerializeField] public GameObject pressPrompt;
    private bool isPlayerNearby = false;
    private bool isObjOpen = false;
    
    private SpriteRenderer spriteRenderer; // Reference to the obj's sprite renderer
    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        // Initially, the obj UI should be hidden
        objUI.SetActive(false);
        pressPrompt.gameObject.SetActive(false);

        // Get the SpriteRenderer component to change color later
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor; // Set to the default color at start
    }

    void Update()
    {
        // Check if player presses "Z" and is near the obj
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isObjOpen)
            {
                SoundManager.PlaySound("open");
                OpenObj();
            }
            else
            {
                SoundManager.PlaySound("close");
                CloseObj();
            }
        }
    }

    // When the player enters the trigger area (near the obj object)
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

    // Function to open the obj
    private void OpenObj()
    {
        pressPrompt.gameObject.SetActive(false); // Remove prompt from screen
        objUI.SetActive(true); // Show the obj UI
        isObjOpen = true;
        // Optional: Pause the game or movement while the obj is open
        Time.timeScale = 0f; // This will pause the game
    }

    // Function to close the obj
    private void CloseObj()
    {
        objUI.SetActive(false); // Hide the obj UI
        isObjOpen = false;
        // Optional: Resume the game or movement if you paused it
        Time.timeScale = 1f; // This will resume the game
    }
}

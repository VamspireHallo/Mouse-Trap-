using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTransition : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // The name of the scene to load
    [SerializeField] private GameObject pressPrompt; // UI prompt to display when near the note
    [SerializeField] private int numOfObjs; // Minimum number of collected objects needed for good ending

    private BoxCollider2D boxCollider; // Reference to the box collider
    private SpriteRenderer spriteRenderer; // Reference to object sprite
    private bool isPlayerNearby = false;

    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = normalColor;

        // Ensure the collider is disabled and the sprite is transparent initially
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0; // Set alpha to 0 (fully transparent)
            spriteRenderer.color = color;
        }

        if (pressPrompt != null)
        {
            pressPrompt.SetActive(false); // Ensure the prompt is initially hidden
        }
    }

    void Update()
    {
        if(InventoryCollection.Instance != null && InventoryCollection.Instance.GetObjsCount() >= numOfObjs)
        {
             if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }

            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 1; // Set alpha to 1 (fully visible)
                spriteRenderer.color = color;
            }

            if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z)) // "Z" to interact
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (spriteRenderer != null) spriteRenderer.color = glowColor;
            if (pressPrompt != null)
            {
                pressPrompt.SetActive(true); // Show the interaction prompt
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (spriteRenderer != null) spriteRenderer.color = normalColor;
            if (pressPrompt != null)
            {
                pressPrompt.SetActive(false); // Hide the interaction prompt
            }
        }
    }
}

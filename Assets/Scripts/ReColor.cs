using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReColor : MonoBehaviour
{
    private bool isPlayerNearby = false;
    
    private SpriteRenderer spriteRenderer; // Reference to the obj's sprite renderer
    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        // Get the SpriteRenderer component to change color later
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor; // Set to the default color at start
    }

    void Update()
    {

    }

    // When the player enters the trigger area (near the obj object)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            spriteRenderer.color = glowColor; // Change to the glowing color
        }
    }

    // When the player exits the trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            spriteRenderer.color = normalColor; // Revert to the normal color
        }
    }
}

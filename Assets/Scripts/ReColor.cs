using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the obj's sprite renderer
    private Image image;
    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        // Get the SpriteRenderer component to change color later
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor; // Set to the default color for SpriteRenderer
        }
        else if (image != null)
        {
            image.color = normalColor; // Set to the default color for Image
        }
    }

    void Update()
    {

    }

    // When the player enters the trigger area (near the obj object)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = glowColor; // Change the color for SpriteRenderer
            }
            else if (image != null)
            {
                image.color = glowColor; // Change the color for Image
            }
        }
    }

    // When the player exits the trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = normalColor; // Revert the color for SpriteRenderer
            }
            else if (image != null)
            {
                image.color = normalColor; // Revert the color for Image
            }
        }
    }
}

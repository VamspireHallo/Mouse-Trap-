using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healthRestoreAmount; // Amount of health to restore
    public Sprite altSprite;         // Alternate sprite to display after pickup
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    private void Start()
    {
        // Cache the SpriteRenderer and Collider2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the Health component (i.e., it's the player)
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Only restore health if the player's current health is below maximum
            if (playerHealth.currentHealth < playerHealth.GetStartingHealth())
            {
                playerHealth.RestoreHealth();
                ReplaceWithAltSprite();
            }
            else
            {
                Debug.Log("Player health is already at maximum. Health pickup ignored.");
            }
        }
    }

    private void ReplaceWithAltSprite()
    {
        // Change to the alternate sprite
        if (altSprite != null)
        {
            spriteRenderer.sprite = altSprite;
        }

        collider.enabled = false;
        StartCoroutine(DestroyAfterDelay(5f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

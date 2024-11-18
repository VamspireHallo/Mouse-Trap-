using System.Collections;
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
        StartCoroutine(FadeOutAndDestroy(5f));
    }

    private IEnumerator FadeOutAndDestroy(float duration)
    {
        float fadeTime = 1f; // Duration of the fade effect
        float fadeRate = Time.deltaTime / fadeTime;

        // Reduce the alpha value over time
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            if (spriteRenderer != null)
            {
                Color currentColor = spriteRenderer.color;
                currentColor.a = Mathf.Clamp01(currentColor.a - fadeRate);
                spriteRenderer.color = currentColor;
            }
            yield return null;
        }

        // Ensure the object is fully transparent
        if (spriteRenderer != null)
        {
            Color finalColor = spriteRenderer.color;
            finalColor.a = 0;
            spriteRenderer.color = finalColor;
        }
        Destroy(gameObject);
    }
}

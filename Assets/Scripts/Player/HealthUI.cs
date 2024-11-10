using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthUI; // Reference to the UI Image component
    [SerializeField] private Sprite normalHealthIcon; // The normal health icon (full heart)
    [SerializeField] private Sprite hurtHealthIcon; // The hurt health icon (damaged heart)

    private Health playerHealth; // Reference to the player's Health script

    void Start()
    {
        // Get the Health component from the player
        playerHealth = FindObjectOfType<Health>();

        // Set the initial health icon to normal
        if (healthUI != null)
        {
            healthUI.sprite = normalHealthIcon;
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            // If health is greater than 1, set the normal icon
            if (playerHealth.currentHealth > 1)
            {
                if (healthUI != null && healthUI.sprite != normalHealthIcon)
                {
                    healthUI.sprite = normalHealthIcon;
                }
            }
            // If health is less than or equal to 1, set the hurt icon
            else if (playerHealth.currentHealth <= 1)
            {
                if (healthUI != null && healthUI.sprite != hurtHealthIcon)
                {
                    healthUI.sprite = hurtHealthIcon;
                }
            }
        }
    }
}

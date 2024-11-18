using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    // Amount of health to restore, set to starting health in the Health script
    public float healthRestoreAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the Health component (i.e., it's the player)
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Only restore health if the player's current health is below maximum
            if (playerHealth.currentHealth < playerHealth.GetStartingHealth())
            {
                // Restore player's health to maximum & destroy object
                playerHealth.RestoreHealth();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player health is already at maximum. Health pickup ignored.");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTrap : Trap
{
    [SerializeField] public float damageRate = 1.0f; // Time between health decrements (seconds)
    public PlayerController playerController;
    public float nextDamageTime = 0f;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.PlaySound("gluetrap");
            base.OnTriggerEnter2D(collision); // Apply initial damage using the parent Trap script

            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                collision.transform.position = transform.position;
                // Stop the player's movement
                playerController.speed = 0;
                nextDamageTime = Time.time + damageRate;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerController != null && Time.time >= nextDamageTime)
        {
            // Decrement health over time
            collision.GetComponent<Health>().TakeDamage(damage);
            nextDamageTime = Time.time + damageRate;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerController != null)
        {
            // Restore the player's movement when they leave the glue trap
            playerController.speed = 3f; // Set this to your player's normal speed
        }
    }
}

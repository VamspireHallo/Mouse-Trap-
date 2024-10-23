using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleTrap : Trap
{
    [SerializeField] public float knockbackForce = 10f; // Strength of the knockback

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Call the parent Trap's damage function

        if (collision.CompareTag("Player"))
        {
            // Apply knockback
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}

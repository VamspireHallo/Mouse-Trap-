using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : Trap
{
    [SerializeField] public float knockbackForce; // Strength of the knockback
    private Animator animator;

    void Start()
    {
        // Initialize the animator
        animator = GetComponent<Animator>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Call the parent Trap's damage function

        if (collision.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetTrigger("CloseTrap");
            }
            
            // Apply knockback
            SoundManager.PlaySound("mousetrap");
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}

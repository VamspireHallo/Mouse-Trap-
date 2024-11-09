using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWay : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    public Animator animManager;
    public PlayerController playerController;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCollider == null)
        {
            playerCollider = GetComponent<BoxCollider2D>();
        }

        animManager = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for falling through one-way platform
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }

        // Set isFalling based on downward movement and if down key is held
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && rb.velocity.y < 0)
        {
            animManager.SetBool("isFalling", true);
        }
        else if (playerController.grounded)
        {
            animManager.SetBool("isFalling", false);  // Reset isFalling when grounded
        }

        // Set isJumping based on upward movement
        if (!animManager.GetBool("isJumping") && IsPlayerJumping())
        {
            animManager.SetBool("isJumping", true);
        }
    }

    private bool IsPlayerJumping()
    {
        // Check upward velocity as a condition for jumping
        return rb.velocity.y > 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
            // Reset isJumping when player exits the platform or lands
            animManager.SetBool("isJumping", false);
        }
    }

    private IEnumerator DisableCollision()
    {
        if (currentOneWayPlatform != null)
        {
            BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

            // Ignore collision to fall through platform
            Physics2D.IgnoreCollision(playerCollider, platformCollider);
            yield return new WaitForSeconds(0.25f);

            // Re-enable collision after falling through
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }
}

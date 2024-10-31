using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWay : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    public Animator animManager;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCollider == null)
        {
            playerCollider = GetComponent<BoxCollider2D>();
        }

        animManager = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for falling through one-way platform
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }

        // Set isJumping based on actual jumping conditions (this might come from a PlayerController)
        if (!animManager.GetBool("isJumping") && IsPlayerJumping())
        {
            animManager.SetBool("isJumping", true);
        }
    }

    private bool IsPlayerJumping()
    {
        // Check upward velocity as a condition for jumping
        return GetComponent<Rigidbody2D>().velocity.y > 0.1f;
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

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
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentOneWayPlatform != null)
        {
            animManager.SetBool("isJumping", true);
            StartCoroutine(DisableCollision());
        }
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
            animManager.SetBool("isJumping", false);
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}

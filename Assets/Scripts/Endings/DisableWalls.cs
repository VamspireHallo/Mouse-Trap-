using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisableWalls : MonoBehaviour
{
    public float disableDuration; // Duration for which the colliders are disabled
    public TextMeshProUGUI messageText;
    private Collider2D[] objectColliders; // Array to store the colliders of the target object

    private void Start()
    {
        // Fetch all colliders attached to the target object
        objectColliders = GetComponents<Collider2D>();
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the player triggers the action
        {
            StartCoroutine(DisableCollidersTemporarily());
            //Destroy(gameObject); // Destroy the trigger box collider
        }
    }

    private IEnumerator DisableCollidersTemporarily()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
        }

        foreach (var collider in objectColliders)
        {
            collider.enabled = false;
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(disableDuration);

        // Re-enable all colliders on this object
        foreach (var collider in objectColliders)
        {
            collider.enabled = true;
        }

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
            Destroy(messageText);
        }
    }
}

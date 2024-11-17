using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image inventoryIcon;                   // Main inventory icon
    [SerializeField] private Color notificationColor = Color.red;   // Color for notification
    [SerializeField] private float flashInterval = 0.3f;            // Interval for flashing
    private Color originalColor;                                    // Store the original color of the icon
    private bool notificationActive = false;                        // Track if the notification is active
    private float notificationDuration = 5f;  
    
    private void Start()
    {
        if (inventoryIcon != null)
        {
            originalColor = inventoryIcon.color; // Save the original color of the icon
        }
    }

    private void Update()
    {
        // Check for "X" key press to stop flashing and reset icon color
        if (notificationActive && Input.GetKeyDown(KeyCode.X))
        {
            ResetIcon();
        }
    }

    // Call this method to start flashing the notification
    public void DisplayNotification()
    {
        if (inventoryIcon != null && !notificationActive)
        {
            notificationActive = true;
            StartCoroutine(FlashIcon()); // Start the flashing coroutine
        }
    }

    // Coroutine to flash the icon color
    private IEnumerator FlashIcon()
    {
        float elapsedTime = 0f;

        while (notificationActive && elapsedTime < notificationDuration)
        {
            inventoryIcon.color = notificationColor;
            yield return new WaitForSeconds(flashInterval);
            inventoryIcon.color = originalColor;
            yield return new WaitForSeconds(flashInterval);

            elapsedTime += 2 * flashInterval; // Increment by two intervals (one cycle of red and original color)
        }

        // Stop flashing after 10 seconds if not already reset
        if (notificationActive)
        {
            ResetIcon();
        }
    }

    // Reset the icon to its original color and stop flashing
    private void ResetIcon()
    {
        if (inventoryIcon != null)
        {
            inventoryIcon.color = originalColor; // Reset to the original color
            notificationActive = false;          // Set notification as inactive
        }
    }
}

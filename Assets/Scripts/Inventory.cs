using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; } // Singleton instance

    private List<GameObject> collectedObjects = new List<GameObject>(); // Stores inventory objects
    private int currentIndex = 0;
    private bool inventoryOpen = false;

    [SerializeField] private GameObject redactedObj; // Reference to the "redacted" object
    [SerializeField] private PlayerController playerController; // Reference to the PlayerController script

    void Start()
    {
        if (redactedObj != null)
        {
            redactedObj.SetActive(false); // Ensure redactedObj is hidden by default
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleInventory();
        }

        if (inventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ScrollInventory(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ScrollInventory(-1);
            }
        }
    }

    // Add inventory object directly to the list
    public void AddToInventory(GameObject inventoryObj)
    {
        if (inventoryObj != null && !collectedObjects.Contains(inventoryObj))
        {
            // Remove the redactedObj if it's the only item in the inventory
            if (collectedObjects.Count == 0 && redactedObj != null)
            {
                redactedObj.SetActive(false);
            }

            collectedObjects.Add(inventoryObj);
        }
    }

    // Toggles the inventory view on and off
    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        if (inventoryOpen)
        {
            ShowCurrentObjUI();
            playerController.enabled = false; // Disable player movement
        }
        else
        {
            HideAllObjUIs();
            playerController.enabled = true; // Re-enable player movement
        }
    }

    // Scrolls through inventory items
    public void ScrollInventory(int direction)
    {
        if (!inventoryOpen) return;

        // If no collected objects, do not scroll
        if (collectedObjects.Count == 0)
        {
            ShowRedactedObj();
            return;
        }

        HideCurrentObjUI();
        currentIndex = (currentIndex + direction + collectedObjects.Count) % collectedObjects.Count;
        ShowCurrentObjUI();
    }

    // Shows the UI of the currently selected inventory item
    private void ShowCurrentObjUI()
    {
        if (collectedObjects.Count > 0)
        {
            GameObject currentObj = collectedObjects[currentIndex];
            currentObj.SetActive(true); // Activate inventory UI for the current object
        }
        else
        {
            ShowRedactedObj(); // Show redacted if no items collected
        }
    }

    // Hides the UI of the currently selected inventory item
    private void HideCurrentObjUI()
    {
        if (collectedObjects.Count > 0)
        {
            GameObject currentObj = collectedObjects[currentIndex];
            currentObj.SetActive(false); // Deactivate inventory UI for the current object
        }
    }

    // Shows the "redacted" object if no items have been collected
    private void ShowRedactedObj()
    {
        if (redactedObj != null)
        {
            redactedObj.SetActive(true);
        }
    }

    // Hides all inventory item UIs when the inventory is closed
    private void HideAllObjUIs()
    {
        foreach (var obj in collectedObjects)
        {
            obj.SetActive(false);
        }

        // Also hide the redacted object when closing the inventory
        if (redactedObj != null)
        {
            redactedObj.SetActive(false);
        }
    }
}

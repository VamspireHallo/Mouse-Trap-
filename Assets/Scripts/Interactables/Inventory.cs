using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryCollection inventoryCollection;
    private int currentIndex = 0;
    private bool inventoryOpen = false;

    [SerializeField] private GameObject redactedObj; // Reference to the "redacted" object
    private PlayerController playerController; // Reference to the PlayerController script

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        inventoryCollection = InventoryCollection.Instance;
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

    // Add inventory object directly to InventoryCollection
    public void AddToInventory(GameObject inventoryObj)
    {
        if (inventoryObj != null)
        {
            inventoryCollection.AddObject(inventoryObj);
            if (redactedObj != null && inventoryCollection.CollectedObjects.Count > 0)
            {
                redactedObj.SetActive(false);
            }
        }
    }

    // Toggles the inventory view on and off
    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        if (inventoryOpen)
        {
            ShowCurrentObjUI();
            Time.timeScale = 0f;
        }
        else
        {
            HideAllObjUIs();
            Time.timeScale = 1f;
        }
    }

    // Scrolls through inventory items
    public void ScrollInventory(int direction)
    {
        if (!inventoryOpen || inventoryCollection.CollectedObjects.Count == 0) return;

        HideCurrentObjUI();
        currentIndex = (currentIndex + direction + inventoryCollection.CollectedObjects.Count) % inventoryCollection.CollectedObjects.Count;
        ShowCurrentObjUI();
    }

    // Shows the UI of the currently selected inventory item
    private void ShowCurrentObjUI()
    {
        if (inventoryCollection.CollectedObjects.Count > 0)
        {
            GameObject currentObj = inventoryCollection.CollectedObjects[currentIndex];
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
        if (inventoryCollection.CollectedObjects.Count > 0)
        {
            GameObject currentObj = inventoryCollection.CollectedObjects[currentIndex];
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
        foreach (var obj in inventoryCollection.CollectedObjects)
        {
            obj.SetActive(false);
        }

        if (redactedObj != null)
        {
            redactedObj.SetActive(false);
        }
    }
}

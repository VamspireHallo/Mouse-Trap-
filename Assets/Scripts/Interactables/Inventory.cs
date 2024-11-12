using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryCollection inventoryCollection;
    private int currentIndex = 0;
    private bool inventoryOpen = false;

    [SerializeField] private GameObject redactedObj; // Reference to the "redacted" object
    [SerializeField] private PlayerController playerController; // Reference to the PlayerController script

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        inventoryCollection = InventoryCollection.Instance;

        Canvas canvas = GameObject.Find("InventoryObjs")?.GetComponent<Canvas>();
        if (canvas != null)
        {
            // Search for the object by name in the Canvas' hierarchy
            redactedObj = canvas.transform.Find("RedactedNote")?.gameObject;  
        }

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
            // If it's the first object, remove the redactedObj from the collection
            if (inventoryCollection.CollectedObjects.Count == 0 && redactedObj != null)
            {
                inventoryCollection.RemoveObject(redactedObj); // Remove redactedObj if it's still in the collection
            }

            // Add the new object to the inventory
            inventoryCollection.AddObject(inventoryObj);

            // Disable redactedObj if any objects are collected
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
            playerController.enabled = false;
            Time.timeScale = 0f;
        }
        else
        {
            HideAllObjUIs();
            playerController.enabled = true;
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

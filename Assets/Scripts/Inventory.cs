using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<int> collectedItemIDs = new List<int>(); // Stores item IDs instead of GameObjects
    private int currentIndex = 0;
    private bool inventoryOpen = false;

    [SerializeField] private PlayerController playerController; // Reference to the PlayerController script
    [SerializeField] private List<GameObject> itemPrefabs; // List of all possible items with InventoryItem components

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

    // Add item ID directly to the list
    public void AddToInventory(int itemID)
    {
        if (!collectedItemIDs.Contains(itemID))
        {
            collectedItemIDs.Add(itemID);
        }
    }

    // Toggles the inventory view on and off
    private void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        if (inventoryOpen)
        {
            ShowCurrentItemUI();
            playerController.enabled = false; // Disable player control
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            HideAllItemUIs();
            playerController.enabled = true; // Enable player control
            Time.timeScale = 1f; // Resume the game
        }
    }

    // Scrolls through inventory items
    private void ScrollInventory(int direction)
    {
        if (!inventoryOpen || collectedItemIDs.Count == 0) return;

        HideCurrentItemUI();
        currentIndex = (currentIndex + direction + collectedItemIDs.Count) % collectedItemIDs.Count;
        ShowCurrentItemUI();
    }

    // Finds an item in itemPrefabs by its ID
    private GameObject GetItemByID(int itemID)
    {
        foreach (GameObject item in itemPrefabs)
        {
            InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
            if (inventoryItem != null && inventoryItem.itemID == itemID)
            {
                return item;
            }
        }
        return null;
    }

    // Shows the UI of the currently selected inventory item
    private void ShowCurrentItemUI()
    {
        if (collectedItemIDs.Count > 0)
        {
            int itemID = collectedItemIDs[currentIndex];
            GameObject currentItem = GetItemByID(itemID);
            if (currentItem != null)
            {
                currentItem.SetActive(true); // Activate inventory UI for the current item
            }
        }
    }

    // Hides the UI of the currently selected inventory item
    private void HideCurrentItemUI()
    {
        if (collectedItemIDs.Count > 0)
        {
            int itemID = collectedItemIDs[currentIndex];
            GameObject currentItem = GetItemByID(itemID);
            if (currentItem != null)
            {
                currentItem.SetActive(false); // Deactivate inventory UI for the current item
            }
        }
    }

    // Hides all inventory item UIs when the inventory is closed
    private void HideAllItemUIs()
    {
        foreach (int itemID in collectedItemIDs)
        {
            GameObject item = GetItemByID(itemID);
            if (item != null)
            {
                item.SetActive(false);
            }
        }
    }
}

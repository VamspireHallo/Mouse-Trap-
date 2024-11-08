using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> collectedObjects = new List<GameObject>(); // Stores inventory objects
    private int currentIndex = 0;
    private bool inventoryOpen = false;

    [SerializeField] private PlayerController playerController; // Reference to the PlayerController script

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
        if (!inventoryOpen || collectedObjects.Count == 0) return;

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

    // Hides all inventory item UIs when the inventory is closed
    private void HideAllObjUIs()
    {
        foreach (var obj in collectedObjects)
        {
            obj.SetActive(false);
        }
    }
}

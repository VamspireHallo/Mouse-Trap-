using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> collectedObjects = new List<GameObject>();
    private int currentIndex = 0;
    private bool inventoryOpen = false;

    [SerializeField] private PlayerController playerController;
    void Update()
    {
        // Toggle the inventory view when the player presses "X"
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleInventory();
        }

        // Check for scrolling input if the inventory is open
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

    // Adds a unique object to the inventory
    public void AddToInventory(GameObject obj)
    {
        if (!collectedObjects.Contains(obj))
        {
            collectedObjects.Add(obj);
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
        }
        else
        {
            HideAllObjUIs();
            playerController.enabled = true;
        }
    }

    // Scrolls through the inventory items
    public void ScrollInventory(int direction)
    {
        if (!inventoryOpen || collectedObjects.Count == 0) return;

        HideCurrentObjUI();

        // Update index and wrap around if needed
        currentIndex = (currentIndex + direction + collectedObjects.Count) % collectedObjects.Count;

        ShowCurrentObjUI();
    }

    // Show objUI of the currently selected item
    private void ShowCurrentObjUI()
    {
        if (collectedObjects.Count > 0)
        {
            GameObject currentObj = collectedObjects[currentIndex];
            InteractableObj interactable = currentObj.GetComponent<InteractableObj>();

            if (interactable != null && interactable.objUI != null)
            {
                interactable.objUI.SetActive(true);
            }
            Time.timeScale = 0f;
        }
    }

    // Hide objUI of the currently selected item
    private void HideCurrentObjUI()
    {
        if (collectedObjects.Count > 0)
        {
            GameObject currentObj = collectedObjects[currentIndex];
            InteractableObj interactable = currentObj.GetComponent<InteractableObj>();

            if (interactable != null && interactable.objUI != null)
            {
                interactable.objUI.SetActive(false);
            }
        }
    }

    // Hide all objUIs in the inventory when closed
    private void HideAllObjUIs()
    {
        foreach (var obj in collectedObjects)
        {
            InteractableObj interactable = obj.GetComponent<InteractableObj>();
            if (interactable != null && interactable.objUI != null)
            {
                interactable.objUI.SetActive(false);
            }
            Time.timeScale = 1f;
        }
    }
}

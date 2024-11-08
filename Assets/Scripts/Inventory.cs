using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; } // Singleton instance

    private List<GameObject> collectedObjects = new List<GameObject>();
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

    public void AddToInventory(GameObject obj)
    {
        if (!collectedObjects.Contains(obj))
        {
            collectedObjects.Add(obj);
        }
    }

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

    public void ScrollInventory(int direction)
    {
        if (!inventoryOpen || collectedObjects.Count == 0) return;

        HideCurrentObjUI();
        currentIndex = (currentIndex + direction + collectedObjects.Count) % collectedObjects.Count;
        ShowCurrentObjUI();
    }

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
        }
    }

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

    private void HideAllObjUIs()
    {
        foreach (var obj in collectedObjects)
        {
            InteractableObj interactable = obj.GetComponent<InteractableObj>();
            if (interactable != null && interactable.objUI != null)
            {
                interactable.objUI.SetActive(false);
            }
        }
    }
}

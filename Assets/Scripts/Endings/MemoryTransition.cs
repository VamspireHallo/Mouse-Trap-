using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTransition : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // The name of the scene to load
    [SerializeField] private GameObject pressPrompt; // UI prompt to display when near the note

    private bool isPlayerNearby = false;

    void Start()
    {
        //gameObject.SetActive(false);
        if (pressPrompt != null)
        {
            pressPrompt.SetActive(false); // Ensure the prompt is initially hidden
        }
    }

    void Update()
    {
        Debug.Log($"Inventory count now: {InventoryCollection.Instance.GetObjsCount()}");
        if(InventoryCollection.Instance != null && InventoryCollection.Instance.GetObjsCount() >= 5)
        {
            Debug.Log($"FOUND: {InventoryCollection.Instance.GetObjsCount()}");
            gameObject.SetActive(true);
            if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z)) // "Z" to interact
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (pressPrompt != null)
            {
                pressPrompt.SetActive(true); // Show the interaction prompt
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pressPrompt != null)
            {
                pressPrompt.SetActive(false); // Hide the interaction prompt
            }
        }
    }
}

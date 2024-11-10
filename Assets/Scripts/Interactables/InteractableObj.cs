using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] public GameObject objUI;
    [SerializeField] public GameObject pressPrompt;
    [SerializeField] public GameObject inventoryObj; // Object to add to InventoryCollection

    private bool isPlayerNearby = false;
    private bool isObjOpen = false;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        // Ensure UI elements are hidden initially
        if (objUI != null) objUI.SetActive(false);
        if (inventoryObj != null) inventoryObj.SetActive(false);
        if (pressPrompt != null) pressPrompt.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = normalColor;

        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // Toggle object interaction when player presses Z
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isObjOpen)
            {
                SoundManager.PlaySound("open");
                OpenObj();
                if (playerController != null) playerController.enabled = false;
            }
            else
            {
                SoundManager.PlaySound("close");
                CloseObj();
                if (playerController != null) playerController.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (spriteRenderer != null) spriteRenderer.color = glowColor;
            if (pressPrompt != null) pressPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (spriteRenderer != null) spriteRenderer.color = normalColor;
            if (pressPrompt != null) pressPrompt.SetActive(false);
        }
    }

    private void OpenObj()
    {
        if (pressPrompt != null) pressPrompt.SetActive(false);
        if (objUI != null) objUI.SetActive(true);

        isObjOpen = true;
        Time.timeScale = 0f;

        // Add object to InventoryCollection if it hasn’t been collected already
        if (InventoryCollection.Instance != null && inventoryObj != null)
        {
            InventoryCollection.Instance.AddObject(inventoryObj);
        }
    }

    private void CloseObj()
    {
        if (objUI != null) objUI.SetActive(false);
        isObjOpen = false;
        Time.timeScale = 1f;
    }
}

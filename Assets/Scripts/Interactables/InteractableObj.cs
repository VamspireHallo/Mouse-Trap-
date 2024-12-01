using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] public GameObject objUI;
    [SerializeField] public GameObject pressPrompt;
    [SerializeField] public string inventoryID;
    [SerializeField] private InventoryUI inventoryUI;
    private GameObject inventoryObj; // Object to add to InventoryCollection

    [SerializeField] private AudioSource audioSource; // Individual audio source
    [SerializeField] private AudioClip openSound;     // Sound for opening the object
    [SerializeField] private AudioClip closeSound;    // Sound for closing the object

    private bool isPlayerNearby = false;
    private bool isObjOpen = false;
    private bool hasNotified = false;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private AudioSource playerAudioSrc;

    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        // Ensure UI elements are hidden initially
        if (objUI != null) objUI.SetActive(false);
        if (pressPrompt != null) pressPrompt.SetActive(false);

        Canvas canvas = GameObject.Find("InventoryObjs")?.GetComponent<Canvas>();
        if (canvas != null)
        {
            // Search for the object by name in the Canvas' hierarchy
            inventoryObj = canvas.transform.Find(inventoryID)?.gameObject; 
            inventoryObj.SetActive(false); 
        }     

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = normalColor;

        playerController = FindObjectOfType<PlayerController>();
        playerAudioSrc = playerController.GetComponent<AudioSource>();
    }

    void Update()
    {
        // Toggle object interaction when player presses Z
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isObjOpen)
            {
                if (openSound != null && audioSource != null)
                {
                    audioSource.clip = openSound;
                    audioSource.Play();
                }
                OpenObj();
                playerAudioSrc.enabled = false;
                if (playerController != null) playerController.enabled = false;
            }
            else
            {
                if (closeSound != null && audioSource != null)
                {
                    audioSource.clip = closeSound;
                    audioSource.Play();
                }
                CloseObj();
                playerAudioSrc.enabled = true;
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
            if (!hasNotified)
            {
                hasNotified = true;
                inventoryUI?.DisplayNotification();
            }
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

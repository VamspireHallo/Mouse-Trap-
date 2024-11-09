using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] public GameObject objUI; 
    [SerializeField] public GameObject pressPrompt;
    [SerializeField] public GameObject inventoryObj; // Unique ID for the object

    private bool isPlayerNearby = false;
    private bool isObjOpen = false;
    private SpriteRenderer spriteRenderer;
    private Inventory playerInventory;
    [SerializeField] private PlayerController playerController;

    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    void Start()
    {
        objUI.SetActive(false);
        inventoryObj.SetActive(false);
        pressPrompt.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor;

        playerInventory = FindObjectOfType<Inventory>(); // Locate the player's inventory
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isObjOpen)
            {
                SoundManager.PlaySound("open");
                OpenObj();
                playerController.enabled = false;
            }
            else
            {
                SoundManager.PlaySound("close");
                CloseObj();
                playerController.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            spriteRenderer.color = glowColor;
            pressPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            spriteRenderer.color = normalColor;
            pressPrompt.SetActive(false);
        }
    }

    private void OpenObj()
    {
        pressPrompt.SetActive(false);
        objUI.SetActive(true);
        isObjOpen = true;
        Time.timeScale = 0f;

        if (playerInventory != null && inventoryObj != null)
        {
            playerInventory.AddToInventory(inventoryObj);
        }
    }

    private void CloseObj()
    {
        objUI.SetActive(false);
        isObjOpen = false;
        Time.timeScale = 1f;
    }
}

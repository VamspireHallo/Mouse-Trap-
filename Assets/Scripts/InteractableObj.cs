using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] public GameObject objUI; 
    [SerializeField] public GameObject pressPrompt;
    private bool isPlayerNearby = false;
    private bool isObjOpen = false;
    
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    private Inventory playerInventory;

    void Start()
    {
        objUI.SetActive(false);
        pressPrompt.gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor;

        // Locate the player's inventory
        playerInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isObjOpen)
            {
                SoundManager.PlaySound("open");
                OpenObj();
            }
            else
            {
                SoundManager.PlaySound("close");
                CloseObj();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            spriteRenderer.color = glowColor;
            pressPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            spriteRenderer.color = normalColor;
            pressPrompt.gameObject.SetActive(false);
        }
    }

    private void OpenObj()
    {
        pressPrompt.gameObject.SetActive(false);
        objUI.SetActive(true);
        isObjOpen = true;
        Time.timeScale = 0f;

        // Add object to inventory
        if (playerInventory != null)
        {
            playerInventory.AddToInventory(gameObject);
        }
    }

    private void CloseObj()
    {
        objUI.SetActive(false);
        isObjOpen = false;
        Time.timeScale = 1f;
    }
}

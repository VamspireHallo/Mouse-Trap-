using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatNotePopUp : MonoBehaviour
{
    [SerializeField] public GameObject objUI;
    [SerializeField] public GameObject pressPrompt;

    [SerializeField] private AudioSource audioSource; // Individual audio source
    [SerializeField] private AudioClip openSound;     // Sound for opening the object
    [SerializeField] private AudioClip closeSound;    // Sound for closing the object

    private bool isPlayerNearby = false;
    private bool isObjOpen = true; // Object starts open
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    [SerializeField] public Color normalColor;
    [SerializeField] public Color glowColor;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (pressPrompt != null) pressPrompt.SetActive(false);
        if (spriteRenderer != null) spriteRenderer.color = normalColor;

        if (objUI != null)
        {
            OpenObj(); // Open the object at the start of the scene
            PlaySound(openSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Allow player to toggle the object's state by pressing Z
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (isObjOpen)
            {
                CloseObj();
                PlaySound(closeSound);
                if (playerController != null) playerController.enabled = true;
            }
            else
            {
                OpenObj();
                PlaySound(openSound);
                if (playerController != null) playerController.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (spriteRenderer != null) spriteRenderer.color = glowColor;
            if (pressPrompt != null && !isObjOpen) pressPrompt.SetActive(true);
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
        if (pressPrompt != null) pressPrompt.SetActive(false); // Hide the prompt when opening the object
        if (objUI != null) objUI.SetActive(true);

        isObjOpen = true;
        Time.timeScale = 0f; // Pause the game when the object is open
    }

    private void CloseObj()
    {
        if (objUI != null) objUI.SetActive(false);

        isObjOpen = false;
        Time.timeScale = 1f; // Resume the game when the object is closed
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

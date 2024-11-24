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
    private bool isObjOpen = false;
    private bool OpeningSceneDone = false;
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

        if (objUI != null && !OpeningSceneDone) 
        {
            /*
            if (openSound != null && audioSource != null)
            {
                audioSource.clip = openSound;
                audioSource.Play();
            }*/
            OpenObj();
        }
    }

    // Update is called once per frame
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
            if (pressPrompt != null && OpeningSceneDone) pressPrompt.SetActive(true);
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
    }

    private void CloseObj()
    {
        if (objUI != null) objUI.SetActive(false);
        isObjOpen = false;
        Time.timeScale = 1f;
        OpeningSceneDone = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CatDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogueLines; // Dialogue lines for the NPC
    [SerializeField] private Animator endScreenAnimator;
    [SerializeField] private Animator catAnimator;
    [SerializeField] private AudioClip catAudioClip;
    private AudioSource catAudioSrc;
    private AudioSource playerAudioSrc;
    private int index;

    public float wordSpeed;
    private bool isPlayerNearby = false;
    [SerializeField] private GameObject pressPrompt;    // UI prompt to show when near NPC
    private PlayerController playerController;
    private bool hasDialogueCompleted = false;

    void Start()
    {
        if (pressPrompt != null)
        {
            pressPrompt.SetActive(false); // Hide the prompt initially
        }
        resetPanel();
        playerController = FindObjectOfType<PlayerController>();
        playerAudioSrc = playerController.GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check for interaction input
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z) && !hasDialogueCompleted) 
        {
            if (dialoguePanel.activeInHierarchy)
            {
                // If typing is complete, go to the next line
                if (dialogueText.text == dialogueLines[index])
                {
                    NextLine();
                }
                else
                {
                    // Finish typing the current line immediately
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[index];
                }
            }
            else
            {
                PlayerUI.SetActive(false);
                pressPrompt.SetActive(false);
                dialoguePanel.SetActive(true);
                if (catAnimator != null)
                {
                    catAnimator.SetTrigger("LookDown");
                }
                //catAudioSrc.PlayOneShot(catAudioClip);
                if (playerController != null)
                {
                    playerAudioSrc.enabled = false;
                    Animator playerAnimator = playerController.GetComponent<Animator>();
                    if (playerAnimator != null)
                    {
                        playerAnimator.SetBool("isMoving", false);
                        playerAnimator.SetBool("isJumping", false);
                        playerAnimator.SetBool("isFalling", false);
                    }
                    playerController.enabled = false;
                }
                StartCoroutine(Typing());
            }
        }
    }


    public void resetPanel()
    {
        index = 0;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if(index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            EndDialogueSequence();
        }
    }

    private void EndDialogueSequence()
    {
        resetPanel();
        hasDialogueCompleted = true;
        if (endScreenAnimator != null)
        {
            SoundManager.PlaySound("endcredits");
            endScreenAnimator.SetTrigger("PlayEnding"); // Trigger End Credits animation
        }

        StartCoroutine(ExitToEndMenu()); // Start transition to the EndMenu scene
    }

    IEnumerator ExitToEndMenu()
    {
        yield return new WaitForSeconds(20f); // Wait for the animation to finish
        SceneManager.LoadScene("EndMenu"); // Load the EndMenu scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialogueCompleted)
        {
            isPlayerNearby = true;
            if (pressPrompt != null) pressPrompt.SetActive(true); // Show interaction prompt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pressPrompt != null) pressPrompt.SetActive(false); // Hide interaction prompt
            resetPanel();
        }
    }
}

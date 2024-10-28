using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] public GameObject pressPrompt;

    private bool isPlayerInTrigger = false; // Check if Player is near trigger zone

    void Start()
    {
        pressPrompt.gameObject.SetActive(false);
    }
    private void Update()
    {
        // Check if player is in the trigger zone and "Z" is pressed
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Set flag when player enters the trigger zone
            pressPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Clear flag when player exits the trigger zone
            pressPrompt.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private bool isPlayerInTrigger = false; // Check if Player is near trigger zone

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Clear flag when player exits the trigger zone
        }
    }
}

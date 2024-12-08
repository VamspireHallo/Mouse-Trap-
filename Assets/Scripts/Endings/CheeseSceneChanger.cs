using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheeseSceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName; // Name of the scene to change to
    [SerializeField] private GameObject pressPrompt; // Reference to the prompt UI

    private bool isPlayerNearby = false;

    void Start()
    {
        // Ensure the prompt starts hidden
        if (pressPrompt != null)
        {
            pressPrompt.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the player is near and presses the 'Z' key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            // Change to the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Show the prompt when the player enters the trigger zone
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (pressPrompt != null)
            {
                pressPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Hide the prompt when the player leaves the trigger zone
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pressPrompt != null)
            {
                pressPrompt.SetActive(false);
            }
        }
    }
}

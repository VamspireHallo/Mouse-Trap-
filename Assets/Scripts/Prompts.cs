using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompts : MonoBehaviour
{
    [SerializeField] public GameObject pressPrompt;
    // Start is called before the first frame update
    void Start()
    {
        pressPrompt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressPrompt.gameObject.SetActive(true);
            Debug.Log("Player entered trigger area, pressPrompt is now active.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressPrompt.gameObject.SetActive(false);
            Debug.Log("Player exited trigger area, pressPrompt is now inactive.");
        }
    }
}

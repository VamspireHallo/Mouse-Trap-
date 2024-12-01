using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemorySceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator transition; // Animator for the transition
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private DialogueMemory dialogueMemory; // Reference to DialogueMemory

    private bool hasSceneChanged = false; // Prevent multiple triggers

    private void Update()
    {
        // Check if dialogue is done and trigger scene change
        if (dialogueMemory != null && dialogueMemory.dialogueDone && !hasSceneChanged)
        {
            hasSceneChanged = true; // Ensure the scene change happens only once
            if (transition != null && !transition.gameObject.activeSelf)
            {
                transition.gameObject.SetActive(true); // Enable the Animator GameObject
            }
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        // Optional: Play close scene sound
        //SoundManager.PlaySound("closescene");

        // Trigger transition animation
        if (transition != null) 
        {
            transition.SetTrigger("isStart");
        }

        // Wait for the transition time
        yield return new WaitForSeconds(transitionTime);

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningMemoryKitchen : MonoBehaviour
{
    [SerializeField] private GameObject objUI; // Reference to the InteractableObj pop up
    [SerializeField] private GameObject catNotePopUp; // Cat note pop up

    [SerializeField] private AudioSource audioSource; // Individual audio source
    [SerializeField] private AudioClip openSound; // Open Sound
    [SerializeField] private AudioClip closeSound; // Close Sound

    [SerializeField] private KeyCode interactionKey = KeyCode.Z; // Key to trigger the interaction

    //private bool isPlayerNearby = false;
    void Start()
    {
        PlaySound(openSound);
        if (catNotePopUp != null) catNotePopUp.SetActive(false);
        if (objUI != null) objUI.SetActive(true);
    }

    private void Update()
    {
        // Listen for the interaction key if the player is near the object
        if (Input.GetKeyDown(interactionKey))
        {
            PlaySound(closeSound);
            if (catNotePopUp != null) catNotePopUp.SetActive(true);
            if (objUI != null) objUI.SetActive(false);
            StartCoroutine(DestroyObj());
        }
    }

    private IEnumerator DestroyObj()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
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

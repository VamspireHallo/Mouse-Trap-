using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartAudio : MonoBehaviour
{
    private AudioSource audioSrc; // Local AudioSource for this GameObject
    [SerializeField] private AudioClip openSceneClip; // Audio clip to play

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        if (audioSrc == null)
        {
            audioSrc = gameObject.AddComponent<AudioSource>();
        }

        // Play the "openscene" clip if assigned
        if (openSceneClip != null)
        {
            audioSrc.PlayOneShot(openSceneClip);
        }
        else
        {
            Debug.LogError("No audio clip assigned to openSceneClip in SceneStartAudio.");
        }
    }
}

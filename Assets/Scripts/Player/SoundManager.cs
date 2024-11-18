using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip PlayerJump;
    public AudioClip CandleHurt;
    public AudioClip MTHurt;
    public AudioClip GlueHurt;
    public AudioClip ObjOpen;
    public AudioClip ObjClose;
    public AudioClip InvOpen;
    public AudioClip InvClose;
    public AudioClip PageTurn;
    public AudioClip HealthPickUp;
    public AudioClip TransitionOpen;
    public AudioClip TransitionClose;

    private static AudioSource audioSrc;
    private static SoundManager instance; // Reference to access non-static variables

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        instance = this;
    }

    public static void StopSound()
    {
        if (audioSrc.isPlaying)
        {
            audioSrc.Stop();
        }
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(instance.PlayerJump);
                break;
            case "candle":
                audioSrc.PlayOneShot(instance.CandleHurt);
                break;
            case "mousetrap":
                audioSrc.PlayOneShot(instance.MTHurt);
                break;
            case "gluetrap":
                audioSrc.PlayOneShot(instance.GlueHurt);
                break;
            case "openobj":
                audioSrc.PlayOneShot(instance.ObjOpen);
                break;
            case "closeobj":
                audioSrc.PlayOneShot(instance.ObjClose);
                break;
            case "openinv":
                audioSrc.PlayOneShot(instance.InvOpen);
                break;
            case "closeinv":
                audioSrc.PlayOneShot(instance.InvClose);
                break;
            case "pageturn":
                audioSrc.PlayOneShot(instance.PageTurn);
                break;
            case "pickup":
                audioSrc.PlayOneShot(instance.HealthPickUp);
                break;

            case "closescene":
                audioSrc.PlayOneShot(instance.TransitionClose);
                break;
            case "openscene":
                audioSrc.PlayOneShot(instance.TransitionOpen);
                break;
            default:
                Debug.LogWarning("Sound clip not found for: " + clip);
                break;
        }
    }
}

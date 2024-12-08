using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadMenuChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator endScreenAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (endScreenAnimator != null)
            {
                endScreenAnimator.SetTrigger("PlayEnding"); // Trigger End Credits animation
            }
           StartCoroutine(ExitToEndMenu());
        }
    }

    IEnumerator ExitToEndMenu()
    {
        yield return new WaitForSeconds(20f); // Wait for the animation to finish
        SceneManager.LoadScene(sceneName); // Load the EndMenu scene
    }
}

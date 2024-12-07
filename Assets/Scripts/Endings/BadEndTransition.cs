using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndTransition : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    void Start()
    {
        // Start the coroutine to handle timing
        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        // Wait for 20 seconds
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene(targetSceneName);
    }
}

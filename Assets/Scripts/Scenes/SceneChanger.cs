using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public Animator transition;
    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           StartCoroutine(LoadLevel());
           //SceneManager.LoadScene(sceneName);     // Normal Loader without transition
        }
    }

    IEnumerator LoadLevel()
    {
        SoundManager.PlaySound("closescene");
        transition.SetTrigger("isStart");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Canvas : MonoBehaviour
{
    private static Scene2Canvas instance;

    private void Awake()
    {
        // If there is already an instance, destroy this one to avoid duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

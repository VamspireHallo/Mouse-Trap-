using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePrompt : MonoBehaviour
{
    [SerializeField] public GameObject pressPrompt;

    // Start is called before the first frame update
    void Start()
    {
        pressPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound("openscene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

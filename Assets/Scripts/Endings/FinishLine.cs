using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] public CatAI catAI;

    // Update is called once per frame
    void Update()
    {
        if (catAI != null)
        {
            catAI.StopAndDisappear();
        }
    }
}

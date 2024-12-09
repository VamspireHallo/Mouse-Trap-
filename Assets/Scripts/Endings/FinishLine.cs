using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] public CatAI catAI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (catAI != null)
            {
                //End Cat & Mouse Chase
                catAI.StopAndDisappear();
            }
        }
    }
}

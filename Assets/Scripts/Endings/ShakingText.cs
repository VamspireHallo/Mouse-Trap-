using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShakingText : MonoBehaviour
{
    public float shakeIntensity = 5f; // How much the text shakes
    public float shakeSpeed = 10f;   // How fast the text shakes
    public bool isShaking = false;   // Control when shaking happens

    private RectTransform rectTransform; // Reference to the Text's RectTransform
    private Vector3 originalPosition;    // Original position of the text

    void Start()
    {
        // Get the RectTransform of the TextMeshPro object
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.localPosition;
        }
        StartShaking();
    }

    void Update()
    {
        if (isShaking && rectTransform != null)
        {
            // Generate random shake effect
            Vector3 shakeOffset = new Vector3(
                Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2 - 1,
                Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2 - 1,
                0f
            ) * shakeIntensity;

            // Apply shake offset
            rectTransform.localPosition = originalPosition + shakeOffset;
        }
        else if (rectTransform != null)
        {
            // Reset to original position if not shaking
            rectTransform.localPosition = originalPosition;
        }
    }

    // Public function to start shaking
    public void StartShaking()
    {
        isShaking = true;
    }

    // Public function to stop shaking
    public void StopShaking()
    {
        isShaking = false;
        if (rectTransform != null)
        {
            rectTransform.localPosition = originalPosition;
        }
    }
}


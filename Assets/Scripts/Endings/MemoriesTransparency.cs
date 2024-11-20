using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoriesTransparency : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private float maxDistance = 5f; // Maximum distance for transparency effect
    [SerializeField] private Renderer objRenderer; // The renderer of the obj object
    [SerializeField] private float minAlpha = 0.1f; // Minimum transparency (0.0 = fully transparent, 1.0 = fully solid)

    private Material objMaterial; // Reference to the material of the obj

    void Start()
    {
        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            SetAlpha(minAlpha); // Start with the obj fully transparent
        }
        else
        {
            Debug.LogError("obj Renderer is not assigned!");
        }
    }

    void Update()
    {
        if (objMaterial == null || player == null) return;

        // Calculate the distance between the player and the obj
        float distance = Vector3.Distance(player.position, transform.position);

        // Calculate alpha based on the distance
        float alpha = Mathf.Clamp(1 - ((distance * 2) / maxDistance), minAlpha, 1.0f);

        // Apply the calculated alpha to the obj's material
        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        if (objMaterial != null)
        {
            Color color = objMaterial.color;
            color.a = alpha;
            objMaterial.color = color;
        }
    }
}

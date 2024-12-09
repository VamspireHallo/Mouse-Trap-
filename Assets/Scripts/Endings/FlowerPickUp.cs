using System.Collections;
using UnityEngine;

public class FlowerPickUp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D flowerCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flowerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (flowerCollider != null)
            {
                flowerCollider.enabled = false;
            }

            StartCoroutine(FadeOutAndDestroy(2f));
        }
    }

    private IEnumerator FadeOutAndDestroy(float duration)
    {
        if (spriteRenderer == null) yield break;

        float fadeRate = 1f / duration; // Adjust fade rate based on duration
        Color originalColor = spriteRenderer.color;

        // Reduce the alpha value over time
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(originalColor.a, 0, t / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the object is fully transparent at the end
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // Disable collider before destroying the object
        if (flowerCollider != null)
        {
            flowerCollider.enabled = false;
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public Animator animManager;

    private bool isDying = false; // Flag to indicate if the player is dying

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        animManager = GetComponent<Animator>();
        GetComponent<PlayerController>().enabled = true;
    }

    public void TakeDamage(float _damage)
    {
        if (isDying) return; // Prevent further actions if the player is dying

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animManager.SetTrigger("isHurt");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isDying) // Ensure Die() is only called once
        {
            StartCoroutine(PlayDeath());
        }
    }

    private IEnumerator PlayDeath()
    {
        isDying = true; // Set the flag to indicate death process
        GetComponent<PlayerController>().enabled = false; // Disable player input
        animManager.SetTrigger("isDead"); // Play death animation
        yield return new WaitForSeconds(1f); // Wait for 1 second
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }

    private void Restart()
    {
        if (!isDying) // Allow restart only if not dying
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Hurt()
    {
        if (!isDying) // Prevent hurt animation if dying
        {
            animManager.SetTrigger("isHurt");
        }
    }

    public void RestoreHealth()
    {
        if (!isDying) // Allow restoring health only if not dying
        {
            currentHealth = startingHealth;
            Debug.Log("Health restored to maximum!");
        }
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }

    // Reload Function for resetting scene
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}

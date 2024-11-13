using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public Animator animManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        animManager = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
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
        // Only reload the scene if needed, without affecting the inventory
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Hurt()
    {
        animManager.SetTrigger("isHurt");
    }

    public void RestoreHealth()
    {
        currentHealth = startingHealth;
        Debug.Log("Health restored to maximum!");
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }

    // Reload Function for resetting scene
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CatAI : MonoBehaviour
{
    public Transform target;

    public float speed = 400f;
    public float nextWaypointDistance = 3f;

    public Transform CatGFX;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private Collider2D catCollider;

    [SerializeField] private int damage = 1; // Damage dealt to the player
    [SerializeField] private float knockbackForce = 20f; // Knockback force
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        catCollider = GetComponent<Collider2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }  
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (path == null)
        {
            return;
        }
        
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(rb.velocity.x >= 0.01f)
        {
            CatGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            CatGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            // Apply damage to the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Apply knockback to the player
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    public void StopAndDisappear()
    {
        // Stop movement
        speed = 0;
        rb.velocity = Vector2.zero;

        if (catCollider != null)
        {
            catCollider.enabled = false;
        }

        // Play disappear animation if available
        if (animator != null)
        {
            animator.SetTrigger("Disappear");
        }

        // Destroy object after short delay
        Destroy(gameObject, 4f); // Adjust delay if you have an animation
    }
}

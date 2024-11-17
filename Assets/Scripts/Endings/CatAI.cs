using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CatAI : MonoBehaviour
{
    public Transform target; // Target to follow
    public float speed = 3f; // Movement speed
    public float jumpForce = 5f; // Jump force
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer mask for the ground
    public LayerMask obstacleLayer; // Layer mask for obstacles
    public float obstacleDetectionDistance = 1f; // Distance to detect obstacles

    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && target != null)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) return;

        // Check if we're grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Move along the path
        if (currentWaypoint < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 velocity = new Vector2(direction.x * speed, rb.velocity.y);

            // Check for obstacles
            bool obstacleDetected = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(velocity.x), obstacleDetectionDistance, obstacleLayer);

            if (obstacleDetected && isGrounded && !isJumping)
            {
                // Jump to avoid the obstacle
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
            }
            else if (isGrounded)
            {
                // Move normally
                rb.velocity = velocity;
                isJumping = false;
            }

            // Update waypoint if close enough
            float distanceToWaypoint = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < 0.5f)
            {
                currentWaypoint++;
            }
        }
    }
}

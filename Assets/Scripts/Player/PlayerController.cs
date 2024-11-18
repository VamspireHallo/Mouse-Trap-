using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject dustPrefab;

    public float speed;
    public float jumpHeight;

    public bool grounded;
    public float jumpTime;
    public bool isRunning;
    public bool isFalling;

    public Animator animManager;
    public Rigidbody2D rb;
    private static AudioSource audioSrc;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        animManager = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 motion = new Vector2(Input.GetAxisRaw("Horizontal"), 0f); // -1 or 1 a = -1 d = 1

        transform.Translate(motion * speed * Time.deltaTime);

        if (transform.position.y < -100f && health != null)
        {
            health.Die();
        }
        
        if(grounded)
        {
            //animManager.SetBool("isJumping", false);
        }

        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.PlaySound("jump");
            animManager.SetBool("isJumping", true);
            rb.velocity = Vector2.zero;
            jumpTime = 0f;
            while(Input.GetKeyDown(KeyCode.Space) && jumpTime < 1f)
            {
                jumpTime += 0.2f;
            }
            rb.AddForce(Vector2.up * jumpHeight * 500 * jumpTime);
            grounded = false;
        }

        if(motion.x != 0)
        {
            isRunning = true;
            animManager.SetBool("isMoving", isRunning);
        }
        else
        {
            isRunning = false;
            animManager.SetBool("isMoving", isRunning);
        }

        if(isRunning)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }  
        }
        else
        {
            audioSrc.Stop();
        }

        if(motion.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(motion.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        animManager.SetFloat("yVelocity", rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OneWayPlatform"))
        {
            grounded = true;
            animManager.SetBool("isJumping", false);
            animManager.SetBool("isFalling", false);
        }
    }

// For falling anim, needs to not colide with the side of the ground
/*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OneWayPlatform"))
        {
            grounded = false;
            animManager.SetBool("isFalling", true);
        }
    }
*/
}

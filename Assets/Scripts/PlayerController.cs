using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject dustPrefab;

    public float speed;
    public float jumpHeight;

    public bool grounded;
    //public bool canDoubleJump;

    public Animator animManager;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        animManager = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 motion = new Vector2(Input.GetAxisRaw("Horizontal"), 0f); // -1 or 1 a = -1 d = 1

        transform.Translate(motion * speed * Time.deltaTime);

        if(grounded && Input.GetKeyDown(KeyCode.Space) 
            //|| !grounded && canDoubleJump && Input.GetKeyDown(KeyCode.Space)
            )
        {
            animManager.SetBool("isJumping", true);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpHeight * 500);

            if(grounded){
                grounded = false;
            }
            else{
                //canDoubleJump = false;
            }
        }

        if(motion.x != 0){
            animManager.SetBool("isMoving", true);
        }
        else{
            animManager.SetBool("isMoving", false);
        }

        if(motion.x < 0){
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(motion.x > 0){
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("ground")){
            grounded = true;
            //canDoubleJump = true;
            animManager.SetBool("isJumping", false);

            GameObject temp = Instantiate(dustPrefab);
            temp.transform.position = transform.position;
        }
    }
}

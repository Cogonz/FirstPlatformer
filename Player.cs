using System.Data.SqlTypes;
using UnityEngine;

public class Player : MonoBehaviour
{
    // How fast we run
    public float runSpeed = 2f;
    
    // How high we can jump
    public float jumpForce = 0.25f;

    public float maxSpeed = 5f;

    public float deathBarrier = -10f;

    public GameObject lose;
    
    private Rigidbody2D rb;
    
    private Animator animator;

    private bool isGrounded = true;

    private Vector3 spawn;
    
    private Transform currentPlatform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawn = transform.position;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (transform.position.y <= deathBarrier)
        {
            Respawn();
            if (ScoreKeeper.GetScore() < 0)
            {
                LoseGame();
            }
        }
    }

    void Movement()
    {
        var move = Input.GetAxis("Horizontal");
        
        rb.linearVelocity = new Vector2(move * runSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.x > maxSpeed)
        {
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocity.y);
        }
        else if (rb.linearVelocity.x  < -maxSpeed)
        {
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocity.y);
        }
        
        var jump = Input.GetButtonDown("Jump");
        if (jump && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
        
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }

        if (collision.gameObject.tag == "MovingPlatform")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            transform.SetParent(collision.transform);
            currentPlatform = collision.transform;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
            currentPlatform = null;
        }
    }

    void Respawn()
    {
        transform.position = spawn;
        rb.linearVelocity = Vector2.zero;
        
        transform.rotation = Quaternion.identity;
        
        ScoreKeeper.AddToScore(-1);
        
    }

    void LoseGame()
    {
        lose.SetActive(true);
        Time.timeScale = 0f;
    }
    
    
}

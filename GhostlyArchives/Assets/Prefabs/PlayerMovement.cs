using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    public float jumpForce = 10f;  // Jump force
    private bool isGrounded;
    public float health = 4f;

    private Rigidbody2D rb;
    private Animator animator = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Physics.SyncTransforms();
        // Horizontal movement with A and D keys
        float moveInput = 0f;


        if (Input.GetButton("Left"))
        {
            moveInput = -1f;
            animator.SetBool("is_Idle", false);
        }
        else if (Input.GetButton("Right"))
        {
            moveInput = 1f;
            animator.SetBool("is_Idle", false);
        }

        // Set horizontal velocity only when there is input
        if (moveInput != 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        // Jumping
        if (Input.GetButton("Jump") && isGrounded)
        {
            // Preserve the current horizontal velocity when jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("is_Idle", false);
        }

        if (moveInput == 0 && isGrounded)
        {
            animator.SetBool("is_Idle", true);  // Enable idle animation when not moving and grounded
        }
        else
        {
            animator.SetBool("is_Idle", false);  // Ensure idle animation is disabled when moving or in the air
        }
    }



    // Check if the player is on the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            health -= 0.5f;
            Destroy(collision.gameObject);
        }
    }
}


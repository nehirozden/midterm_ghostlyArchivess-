using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    public float jumpForce = 10f;  // Jump force
    private bool isGrounded;
    public float health = 5f;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Physics.SyncTransforms();
        // Horizontal movement with A and D keys
        float moveInput = 0f;

        if (Input.GetButton("Left"))
        {
            moveInput = -1f;
            spriteRenderer.flipX = true;
        }
        else if (Input.GetButton("Right"))
        {
            moveInput = 1f;
            spriteRenderer.flipX = false;
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
        }

    }



    // Check if the player is on the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = Vector2.zero;
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
            health -= 1.0f;
            Destroy(collision.gameObject);
        }
    }
}
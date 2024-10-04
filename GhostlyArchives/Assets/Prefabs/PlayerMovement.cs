using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Movement speed
    public float jumpForce = 10f;  // Jump force
    private bool isGrounded;

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

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        // Set horizontal velocity only when there is input
        if (moveInput != 0)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

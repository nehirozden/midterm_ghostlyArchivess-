using UnityEngine;

public class BookController : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public float acceleration = 10.0f;  // Acceleration of the book's speed
    private Rigidbody2D rb;
    private float speed = 0f;  // Initial speed
    private bool isMoving = false;

    private Collider2D playerCollider;  // Player's collider
    private Collider2D bookCollider;    // Book's collider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the colliders of the book and the player
        playerCollider = player.GetComponent<Collider2D>();
        bookCollider = GetComponent<Collider2D>();

        // Ignore collision between the book and the player
        Physics2D.IgnoreCollision(bookCollider, playerCollider);
    }

    void Update()
    {
        // Check if 'P' is being held down
        if (Input.GetKey(KeyCode.P))
        {
            isMoving = true;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            // Stop moving when the 'P' key is released
            isMoving = false;
            rb.velocity = Vector2.zero;  // Stop the book's movement
            speed = 0f;  // Reset the speed
        }

        // Move the book if 'P' is held down
        if (isMoving)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction from the book to the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Increase the speed over time
        speed += acceleration * Time.deltaTime;

        // Apply velocity towards the player
        rb.velocity = direction * speed;
    }
}

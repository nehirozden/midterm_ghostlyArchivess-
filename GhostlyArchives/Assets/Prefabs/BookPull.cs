using UnityEngine;

public class BookController : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public float acceleration = 10.0f;  // Acceleration of the book's speed
    public float launchForce = 10.0f;   // Force applied when launching
    public float launchDistanceThreshold = 2.0f; // Distance to launch

    private Rigidbody2D rb;
    private Collider2D playerCollider; // Player's collider
    private Collider2D bookCollider;   // Book's collider
    private float speed = 0f;  // Initial speed
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the colliders of the book and the player
        bookCollider = GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();

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
            // Check the distance to the player
            if (Vector2.Distance(transform.position, player.transform.position) <= launchDistanceThreshold)
            {
                speed = 0f; 
                LaunchTowardsMouse(); // Launch if close to player
            }
            else
            {
                // Stop moving when the 'P' key is released
                isMoving = false;
                rb.velocity = Vector2.zero;  // Stop the book's movement
                speed = 0f;  // Reset the speed
            }
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

    void LaunchTowardsMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're working in 2D

        // Calculate the direction to the mouse
        Vector2 launchDirection = (mousePosition - transform.position).normalized;

        // Apply a force in the launch direction
        rb.velocity = launchDirection * launchForce;

        // Optionally, reset isMoving if you want to stop the book from moving after launching
        isMoving = false;
    }
}

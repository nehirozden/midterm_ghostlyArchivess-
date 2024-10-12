using UnityEngine;

public class BookController : MonoBehaviour
{

    public bool isSelected = false; // Whether this book is selected

    public GameObject player;  // Reference to the player object
    public float acceleration = 20.0f;  // Acceleration of the book's speed
    public float launchForce = 7.0f;   // Force applied when launching
    public float oomph = 0.5f; // Force added by player
    public float launchDistanceThreshold = 2.0f; // Distance to launch


    private Rigidbody2D rb;
    private Collider2D playerCollider; // Player's collider
    private Collider2D bookCollider;   // Book's collider
    private float speed = 0f;  // Initial speed
    public bool isMoving = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        // Get the colliders of the book and the player
        bookCollider = GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();

        // Ignore collision between the book and the player
        Physics2D.IgnoreCollision(bookCollider, playerCollider);
    }

    void Update()
    {
        // Selection code 
      
        if (Input.GetButtonDown("Select")) {
            if (isSelected) {
                Debug.Log("deselected");
                isSelected = false;
            }
        }


        // Check if M1 is being held down
        if (Input.GetButton("Pull"))
        {
            if (isSelected) {
                if (Vector2.Distance(transform.position, player.transform.position) <= launchDistanceThreshold)
                {
                    transform.position = player.transform.position;
                }
                else {
                    // Move the book if m1 is held down
                    if (isMoving)
                    {
                        MoveTowardsPlayer();
                    }
                    isMoving = true;

                }
            }
            else {
                isMoving = false;
                speed = 0f;
            }
        }

        // Release the key
        else if (Input.GetButtonUp("Pull"))
        {
            if (isSelected) {

                // Check the distance to the player
                if (Vector2.Distance(transform.position, player.transform.position) <= launchDistanceThreshold)
                {
                    speed = 0f; 
                    LaunchTowardsMouse(); // Launch if close to player
                }
            }
            // Stop moving when the m1 key is released
            isMoving = false;
            speed = 0f;  // Reset the speed
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
        // Get mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 directionToMouse = (mousePosition - transform.position).normalized;
        directionToMouse = directionToMouse.normalized;


        // Get current player velocity
        Vector2 playerVelo = player.GetComponent<Rigidbody2D>().velocity;
        float velocityInMouseDirection = Vector2.Dot(playerVelo, directionToMouse);

        // Apply the launch force 
        rb.velocity = directionToMouse * (launchForce + velocityInMouseDirection * oomph); 

        isMoving = false;
    }
    
    void OnMouseOver() {     
        if (Input.GetButtonUp("Select")){
            isSelected = true;
        }
    }

    // For the bookshelf to set selected
    public void bookshelfSelect() {
        isSelected = true;
    }
}

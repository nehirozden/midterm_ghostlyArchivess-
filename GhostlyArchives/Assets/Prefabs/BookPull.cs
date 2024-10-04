using UnityEngine;

public class BookController : MonoBehaviour
{

    public bool isSelected = false; // Whether this book is selected

    public GameObject player;  // Reference to the player object
    public float acceleration = 20.0f;  // Acceleration of the book's speed
    public float launchForce = 10.0f;   // Force applied when launching
    public float launchDistanceThreshold = 2.0f; // Distance to launch


    private Rigidbody2D rb;
    private Collider2D playerCollider; // Player's collider
    private Collider2D bookCollider;   // Book's collider
    private float speed = 0f;  // Initial speed
    private bool isMoving = false;

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
      
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (isSelected) {
                Debug.Log("deselected");
                isSelected = false;
            }
        }


        // Check if M1 is being held down
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Q))
        {
            if (isSelected) {
                if (Vector2.Distance(transform.position, player.transform.position) <= launchDistanceThreshold)
                {
                    Debug.Log("Close to player");

                    transform.position = player.transform.position;
                }
                else {
                    // Move the book if m1 is held down
                    if (isMoving)
                    {
                        Debug.Log("Moving towards player update");

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
        else if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Q))
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
        Debug.Log("Moving towards player");
        // Calculate the direction from the book to the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Increase the speed over time
        speed += acceleration * Time.deltaTime;

        // Apply velocity towards the player
        rb.velocity = direction * speed;
    }


    void LaunchTowardsMouse()
    {
        Debug.Log("Launching towards mouse");

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Calculate the direction to the mouse
        Vector2 launchDirection = (mousePosition - transform.position).normalized;

        // Apply a force in the launch direction
        rb.velocity = launchDirection * launchForce;
        isMoving = false;
    }
    
    void OnMouseOver() {
        
        if (Input.GetMouseButtonUp(0)){
            Debug.Log(player.transform.position);

            Debug.Log("selected");

            isSelected = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAI : MonoBehaviour
{

    public float Speed = 3f;
    private Transform target;
    public float StopChase;
    private Rigidbody2D rb;

    public GameObject player;
    public GameObject mothPrefab; // Reference to the Moth prefab

    private Collider2D playerCollider;  // Player's collider
    private Collider2D bookCollider;    // Book's collider
    private Collider2D mothCollider;    // Moth's collider


    public float requiredVelocity = 10f; // Velocity needed to kill the moth

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 

        rb = GetComponent<Rigidbody2D>();

        // Get the colliders of the book and the player
        playerCollider = player.GetComponent<Collider2D>();
        mothCollider = GetComponent<Collider2D>();

        // Ignore collision between the moth and the player
        Physics2D.IgnoreCollision(mothCollider, playerCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > StopChase) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object we collided with has the "Book" tag
        if (collision.CompareTag("Book"))
        {
            // Get the velocity of the book's Rigidbody2D
            Rigidbody2D bookRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (bookRb != null)
            {
                // Check if the book's velocity is greater than the required velocity
                if (bookRb.velocity.magnitude >= requiredVelocity)
                {
                    Debug.Log(bookRb.velocity.magnitude);
                    // Destroy the moth if the book is fast enough
                    Destroy(gameObject);
                    Debug.Log("Moth destroyed by the book!");
                }
                else {
                    Debug.Log("Not fast enough!");

                }
            }
        }
    }
}

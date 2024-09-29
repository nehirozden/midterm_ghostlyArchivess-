using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAI : MonoBehaviour
{

    public float Speed; private
    Transform target; public
    float StopChase;
    private Rigidbody2D rb;

    public GameObject player;

    private Collider2D playerCollider;  // Player's collider
    private Collider2D bookCollider;    // Book's collider

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 


        rb = GetComponent<Rigidbody2D>();

        // Get the colliders of the book and the player
        playerCollider = player.GetComponent<Collider2D>();
        bookCollider = GetComponent<Collider2D>();

        // Ignore collision between the book and the player
        Physics2D.IgnoreCollision(bookCollider, playerCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > StopChase) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }

    }
}

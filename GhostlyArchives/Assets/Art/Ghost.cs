using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Vector2 movement;
    private bool isGrounded = true; // Check if the object is on the ground

    // Auto-load the RigidBody component into the variable:
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Listen for player input to move the object:
    void FixedUpdate(){
        movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Check for jump input in Update (non-physics based checks):
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            Jump();
        }
    }

    // Apply a vertical force to simulate jumping:
    void Jump(){
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false; // Once jumped, it's not grounded anymore
    }

    // Optional: Detect when the object hits the ground
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f) {
            isGrounded = true; // Detect if the object is on the ground again
        }
    }
}
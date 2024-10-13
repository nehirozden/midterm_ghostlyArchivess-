using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBook : MonoBehaviour
{
    public bool hasBook = false;  // To store the book reference
    private bool isSelected = false;  // To check if the bookshelf is selected

    // Code for managing the sprites
    public SpriteRenderer spriteRenderer;
    public Sprite full_bookshelf;
    public Sprite empty_bookshelf;

    public GameObject bookPrefab; // Prefab of the book to spawn

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (hasBook) {
            spriteRenderer.sprite = full_bookshelf;
        }
        else {
            spriteRenderer.sprite = empty_bookshelf;
        }
    }

    void Update()
    {
        if (hasBook) {
            spriteRenderer.sprite = full_bookshelf;
        }
        else {
            spriteRenderer.sprite = empty_bookshelf;
        }

        // Selection script
        if (Input.GetButtonDown("Select")) {
            if (isSelected) {
                Debug.Log("deselected");
                isSelected = false;
            }
        }
        // Check if the bookshelf is selected
        if (isSelected && Input.GetButtonDown("Pull"))
        {
            SpawnBook();
        }
    }


    // Detect when a book collides with the bookshelf
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Book"))
        {
            Debug.Log(other.GetComponent<BookController>().isMoving);
            if (!hasBook && !other.GetComponent<BookController>().isMoving) {
                hasBook = true;
                spriteRenderer.sprite = full_bookshelf;

                // Destroy the book from the scene
                Destroy(other.gameObject);
            }
        }
    }

    // Deselecting
    private void OnMouseUpAsButton()
    {
        // Toggle selection state when the bookshelf is clicked
        isSelected = !isSelected;

        // if (isSelected)
        // {
        //     Debug.Log("Bookshelf selected. Press 'Q' to spawn the book.");
        // }
        // else
        // {
        //     Debug.Log("Bookshelf deselected.");
        // }
    }

    // Spawn the book from the bookshelf
    void SpawnBook()
    {
        if (hasBook) {
            // Instantiate a new book from the stored book's prefab
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= 1.0f;
            GameObject newBook = Instantiate(bookPrefab, spawnPosition, Quaternion.identity);

            // Set the selected of the book to be true
            BookController bookController = newBook.GetComponent<BookController>();
            bookController.bookshelfSelect();

            isSelected = false;

            hasBook = false;
            spriteRenderer.sprite = empty_bookshelf;
        }
    }

    // Select
    void OnMouseOver() {
        if (Input.GetButtonUp("Select")){
            isSelected = true;
        }
    }
}

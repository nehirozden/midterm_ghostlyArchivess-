using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBook : MonoBehaviour
{
    private bool hasBook = true;  // To store the book reference
    private bool isSelected = false;  // To check if the bookshelf is selected

    public GameObject bookPrefab; // Prefab of the book to spawn

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (isSelected) {
                Debug.Log("deselected");
                isSelected = false;
            }
        }
        // Check if the bookshelf is selected
        if (isSelected && Input.GetKeyDown(KeyCode.Q))
        {
            SpawnBook();
        }
    }

    // Detect when a book collides with the bookshelf
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Book"))
        {
            if (!hasBook) {
                hasBook = true;
                // Destroy the book from the scene
                Destroy(other.gameObject);

                Debug.Log("Book stored in the bookshelf");
            }
        }
    }

    // This function handles selection when the bookshelf is clicked
    private void OnMouseUpAsButton()
    {
        // Toggle selection state when the bookshelf is clicked
        isSelected = !isSelected;

        if (isSelected)
        {
            Debug.Log("Bookshelf selected. Press 'Q' to spawn the book.");
        }
        else
        {
            Debug.Log("Bookshelf deselected.");
        }
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
            Debug.Log("Book spawned from bookshelf");            

            hasBook = false;
        }
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonUp(0)){
            Debug.Log("shelf selected");

            isSelected = true;
        }
    }
}

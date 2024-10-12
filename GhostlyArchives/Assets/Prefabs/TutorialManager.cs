using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Use this if you're using the standard UI Text
using UnityEngine.SceneManagement; // Required to get the active scene

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText; // Reference to the Text component
    // public TMP_Text tutorialText; // Uncomment if you're using TextMeshPro

    private Queue<string> tutorialMessages; // Queue to hold the tutorial messages
    private bool isTutorialActive = true; // Flag to check if the tutorial is active

    void Start()
    {
        // Initialize the queue
        tutorialMessages = new Queue<string>();

        // Determine the current level (scene) and load the corresponding messages
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            tutorialMessages.Enqueue("Welcome to Ghostly Archives!\n(R to continue)");
            tutorialMessages.Enqueue("AD or Arrow Keys to move, W, Space, or Up to jump");
            tutorialMessages.Enqueue("Click a book to select it");
            tutorialMessages.Enqueue("Hold Q or right click to pull it");
            tutorialMessages.Enqueue("Let go when it's close to launch the book!");
            tutorialMessages.Enqueue("Now try to shelve all the books.");

        }
        else if (currentScene == "Level2")
        {
            tutorialMessages.Enqueue("Moths are coming");
            tutorialMessages.Enqueue("Hit them with a book hard enough!");
        }
        else if (currentScene == "Level3")
        {

        }

        // Start the tutorial
        DisplayNextMessage();
    }

    void Update()
    {
        // Check for space bar input to display the next message
        if (isTutorialActive && Input.GetKeyDown(KeyCode.R))
        {
            DisplayNextMessage();
        }
    }

    void DisplayNextMessage()
    {
        if (tutorialMessages.Count == 0)
        {
            EndTutorial();
            return;
        }

        // Get the next message from the queue
        string message = tutorialMessages.Dequeue();
        tutorialText.text = message; // Update the text with the current message
    }

    void EndTutorial()
    {
        tutorialText.text = ""; // Clear the tutorial text
        isTutorialActive = false; // Mark the tutorial as finished
        // Add any additional logic for when the tutorial ends
    }
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    public GameObject healthText;
    public PlayerController currHealth;

    // Method to start the game (load the start scene)
    public void StartScene(){
        // As in, when the start button is clicked, this scene is loaded
        SceneManager.LoadScene("KevinScene"); // Change this to your start scene name
        Debug.Log("in start");
        updateStatsDisplay();
        Debug.Log("after display");
    }

    // Method to restart the game
    public void RestartGame(){
        // As in, when the restart button is clicked, this scene is loaded
        SceneManager.LoadScene("KevinScene"); // Change this to the scene you want to restart
    }

    // Method to quit the game
    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Update is called once per frame
    void Update(){
        updateStatsDisplay();
        // Checks if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)){
            QuitGame(); // Call the QuitGame function when Escape is pressed
        }
    }

    public void updateStatsDisplay(){
        Debug.Log("here");
        Text healthTextTemp = healthText.GetComponent<Text>();
        // currHealth = GetComponent<PlayerController>();
        Debug.Log("after");


        healthTextTemp.text = "HEALTH: " + currHealth.health;
        Debug.Log(currHealth.health);
    }


}
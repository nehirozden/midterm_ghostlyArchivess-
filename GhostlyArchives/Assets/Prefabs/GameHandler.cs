using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    public GameObject healthText;
    public PlayerController currHealth;

    public string[] levels;
    public string[] transitions;
    private int curr_level;
    private GameObject[] shelves;

    
    void Start() {
        levels = new string[] {"Level1", "Level2", "Level3"};
        transitions = new string[] {"Transition1", "Transition2"};

        // In order to store the last level the player was at before dying
        curr_level = PlayerPrefs.GetInt("currentLevel", 0);
    }
 
    // Method to start the game (load the start scene)
    public void StartScene(){
        curr_level = 0;

        curr_level = PlayerPrefs.GetInt("currentLevel", curr_level);
        // As in, when the start button is clicked, this scene is loaded
        SceneManager.LoadScene(levels[0]); // Change this to your start scene name
    }

    public void GameOverScene(){
        curr_level = PlayerPrefs.GetInt("currentLevel", curr_level);
        SceneManager.LoadScene("EndScene");
    }

    // Method to restart the game
    public void RestartGame(){
        curr_level = PlayerPrefs.GetInt("currentLevel");
        // As in, when the restart button is clicked, this scene is loaded
        SceneManager.LoadScene(levels[curr_level]);
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
        // Add health if in a level
        string currentSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < levels.Length; i++)
        {
            if (currentSceneName == levels[i])
            {
                curr_level = i;
                updateStatsDisplay();
                // Saving the current level
                PlayerPrefs.SetInt("currentLevel", curr_level);

                if (checkIfComplete()) {
                    load_next();
                }
            }
        }
        
        // Checks if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)){
            QuitGame(); // Call the QuitGame function when Escape is pressed
        }
    }

    // Checks level completion
    bool checkIfComplete() {
        shelves = GameObject.FindGameObjectsWithTag("Bookshelf");
        foreach (GameObject shelf in shelves) {
            SelectBook shelf_script = shelf.GetComponent<SelectBook>();
            if (shelf_script == null || !shelf_script.hasBook) {
                return false;
            }
        }
        return true;
    }    

    public void load_next() {
        if (curr_level < levels.Length - 1) {
            curr_level++;
            PlayerPrefs.SetInt("currentLevel", curr_level);
            SceneManager.LoadScene(transitions[curr_level - 1]);
        }
        else {
            SceneManager.LoadScene("FinalScene");
        }
    }

    public void updateStatsDisplay(){
        currHealth = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        Text healthTextTemp = healthText.GetComponent<Text>();

        if (currHealth.health == 0) {
            GameOverScene();
        } else {
            healthTextTemp.text = "HEALTH: " + currHealth.health;
        }
    }


}


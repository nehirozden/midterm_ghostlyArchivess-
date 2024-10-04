using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    // Method to restart the game
    public void RestartGame(){
        SceneManager.LoadScene("NehirScene"); // Change this to the scene you want to restart
    }

    // Method to quit the game
    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
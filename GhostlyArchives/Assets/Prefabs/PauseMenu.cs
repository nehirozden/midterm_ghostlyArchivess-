using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    void Start() {
        Debug.Log("starting in pauseMenu");
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Debug.Log("Pausing");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}

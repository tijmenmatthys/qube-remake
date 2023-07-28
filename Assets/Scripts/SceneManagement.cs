using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;

    private void Start()
    {
        if (IsGameActive()) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            clickSound.Play();
            if (IsMenuActive()) QuitGame();
            else if (IsGameActive()) LaunchMenu();
       } 
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LaunchMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        //if (Application.isEditor)
        //    UnityEditor.EditorApplication.isPlaying = false;
    }

    private bool IsMenuActive()
    {
        return SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu");
    }

    private bool IsGameActive()
    {
        return SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game");
    }
}

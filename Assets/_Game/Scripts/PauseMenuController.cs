using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject menu;
    private bool isPaused;
    private float originalFixedTimeDeltaTime;


    void Start()
    {
        isPaused = false;
        originalFixedTimeDeltaTime = Time.fixedDeltaTime;
        menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = originalFixedTimeDeltaTime;
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    public void LoadMenu()
    {
        UnpauseGame();
        SceneManager.LoadScene(1);
    }


}

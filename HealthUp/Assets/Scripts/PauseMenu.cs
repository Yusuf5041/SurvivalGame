using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, hudUI;
    public Animator anim;
    // Update is called once per frame
    //check if escape key pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("hit escape");
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    //resume game time and hide menu
    public void Resume()
    {
        GameIsPaused = false;
        anim.Play("Exit Panel Out");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //pauseMenuUI.SetActive(false);
        //hudUI.SetActive(true);
        if (TutorialHandler.inTutorial)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
        
        Debug.Log("resumed");
    }

    //show pause menu and pause game
    public void Pause()
    {
        GameIsPaused = true;
        anim.Play("Exit Panel In");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //hudUI.SetActive(false);
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0.2f;
        
    }

    //load main menu screen
    public void LoadMainMenuSpeed()
    {
        Time.timeScale = 1f;
    }

    //quit game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

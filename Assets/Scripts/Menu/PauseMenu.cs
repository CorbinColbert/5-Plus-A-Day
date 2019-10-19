using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; //Variable to check if paused

    public GameObject PauseMenuUI;//The game object for the Pause Menu

    // Update is called once per frame
    void Update()//Checks if player has pressed ESC to go to Pause Menu
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    public void Resume()//Resumes the active game 
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //Game speed is normal rate
        isPaused = false;
    }

    void Pause()//Pauses the active game
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Freezes game
        isPaused = true;
    }

    public void LoadMenu()//Loads the main menus
    {
        SceneManager.LoadScene("Menus");//Create a variable for the MainMenu scene
        Time.timeScale = 1f; //Game speed is normal rate
        Debug.Log("Loading Menu...");
    }

    //Testing, need to add save method 
    public void SaveLoadMenu()//Needs to be fixed with save method
    {
        //Need save method
        Debug.Log("Saving Game Data...");

        SceneManager.LoadScene("Menus");//Create a variable for the MainMenu scene
        Time.timeScale = 1f; //Game speed is normal rate
        Debug.Log("Loading Menu...");
    }

    public void SaveQuitGame()//Exits the game
    {
        //Need save method
        Debug.Log("Saving Game Data...");

        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void QuitGame()//Exits the game
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}

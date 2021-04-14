using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    /// <summary>
    /// Allows the function of playing the game when pressing the play button in the menu
    /// as well as makes sure the game is not paused
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        PauseMenu.GameIsPaused = false;
    }

    /// <summary>
    /// Allows the functionality of loading up the menu through a button as well as makes sure the game
    /// is not paused when loading other scenes
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScene");
        PauseMenu.GameIsPaused = false;
    }

    /// <summary>
    /// Allows the player to quit the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

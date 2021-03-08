using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // loads the scene at index 1
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

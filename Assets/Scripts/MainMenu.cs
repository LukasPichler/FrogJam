using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Loader.Load("LVL001");
    }
    
    public void PlayTutorial()
    {
        Loader.Load("Tutorial");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Loader.Load("Menu");
    }
}

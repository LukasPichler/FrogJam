using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        CalculateScore.Score = 0;
        PlayerPrefs.DeleteKey("LVL");
        Loader.Load("LVL001");
    }
    
    public void PlayTutorial()
    {
        CalculateScore.Score = 0;
        Loader.Load("Tutorial");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SaveFile.DeleteInput();
        Loader.Load("Menu");
    }
}

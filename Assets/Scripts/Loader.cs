using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader 
{

    private static Action onLoaderCallback;

    public static void Load(int index)
    {
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(index);
        };
        SceneManager.LoadScene("LoadingScene");
    }


    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }


}

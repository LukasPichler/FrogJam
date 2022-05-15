using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    private int _currentLvl;

    [SerializeField]
    private GameObject button;


    // Start is called before the first frame update
    void Start()
    {
        _currentLvl = PlayerPrefs.GetInt("LVL", 0);
        if(_currentLvl == 0)
        {
           button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }
    }

    public void GoToCurrentLVL()
    {
        Loader.Load(_currentLvl);
    }
}

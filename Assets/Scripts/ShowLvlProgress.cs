using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowLvlProgress : MonoBehaviour
{

    private TMP_Text _text;

    [SerializeField]
    private int _nrOfLVL;

    [SerializeField]
    private int _LVLNrStart;


    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int currentLVL = (SceneManager.GetActiveScene().buildIndex - _LVLNrStart);
        if (currentLVL <= 0)
        {
            _text.text = "";
        }
        else
        {
            _text.text = currentLVL + "/" + _nrOfLVL;
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    private TMP_Text _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _textMesh.text = "Score: " + CalculateScore.Score;
    }

  
}

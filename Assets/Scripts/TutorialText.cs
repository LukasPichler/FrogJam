using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMesh;

    private float _clock = 0f;

    [Serializable]
    private class TutorialTuple
    {
        public float TimeShowingText;
        public string Text;
    }

    [SerializeField]
    private List<TutorialTuple> tutorialTuple = new List<TutorialTuple>();

    private int currentTuple = 0;

   

    // Update is called once per frame
    void Update()
    {
        

        if (currentTuple >= tutorialTuple.Count)
        {
            Debug.Log("Disable Text");
        }
        else
        {
            _clock += Time.deltaTime;
            if (_clock >= tutorialTuple[currentTuple].TimeShowingText)
            {
                _clock = 0f;
                currentTuple++;
            }
            textMesh.text = tutorialTuple[currentTuple].Text;
        }
    }
}

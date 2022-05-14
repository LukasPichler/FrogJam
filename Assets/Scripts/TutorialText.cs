using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMesh;
    [SerializeField]
    private GameObject _textBox;

    [SerializeField]
    private SpriteRenderer _spriteOfTalkingBuble;

    private float _clock = 0f;

    [Serializable]
    private class TutorialTuple
    {

        public Vector2 SizeOfTalkingBuble;
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
            _textBox.SetActive(false);
        }
        else
        {
            _clock += Time.deltaTime;
            if ( _clock >= tutorialTuple[currentTuple].TimeShowingText)
            {
                _clock = 0f;
                currentTuple++;
                if (currentTuple < tutorialTuple.Count)
                {
                    _spriteOfTalkingBuble.size = tutorialTuple[currentTuple].SizeOfTalkingBuble;
                }
            }
            if(currentTuple < tutorialTuple.Count)
            {
                textMesh.text = tutorialTuple[currentTuple].Text;
            }
        }
    }
}

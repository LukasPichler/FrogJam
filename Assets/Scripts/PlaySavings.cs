using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySavings : MonoBehaviour
{

    private SaveMovement _save;
    private Movement _movement;
    [SerializeField]
    private bool _replay = false;
    private float _clock;

    private int currentJump=0;
    private int currentRotate=0;


    private void Awake()
    {
        _save = GetComponent<SaveMovement>();
        _movement = GetComponent<Movement>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (_replay)
        {
            _clock += Time.deltaTime;
            if(currentJump < _save.Jump.Count && _save.Jump[currentJump].Time <= _clock)
            {
                _movement.Jump(_save.Jump[currentJump].Value);
                currentJump++;
            }
            if (currentRotate < _save.Rotation.Count && _save.Rotation[currentJump].Time <= _clock)
            {
                _movement.Rotate(_save.Rotation[currentJump].Value);
                currentRotate++;
            }
        }
    }
}

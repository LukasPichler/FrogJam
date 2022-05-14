using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySavings : MonoBehaviour
{
    [SerializeField]
    public int number;
    
    private Movement _movement;
    [SerializeField]
    private bool _replay = false;
    private float _clock;

    private int currentJump=0;
    private int currentRotate=0;
    [SerializeField]
    private List<SaveMovement.Tupel> _rotation = new List<SaveMovement.Tupel>();
    private List<SaveMovement.Tupel> _jump = new List<SaveMovement.Tupel>();

    public bool IsHolding = false;

    private void Awake()
    {
        if (SaveFile.saveMovementsJump.Count > number)
        {
            _jump = new List<SaveMovement.Tupel>(SaveFile.saveMovementsJump[number]);
        }
        if (SaveFile.saveMovementsRotate.Count > number)
        {
            _rotation = new List<SaveMovement.Tupel>(SaveFile.saveMovementsRotate[number]);
        }
        _movement = GetComponent<Movement>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (_replay)
        {
            _clock += Time.deltaTime;
            if(currentJump < _jump.Count && _jump[currentJump].Time - _jump[currentJump].Value <= _clock)
            {
                IsHolding = true;
            }
            else
            {
                IsHolding = false;
            }

            if(currentJump < _jump.Count && _jump[currentJump].Time <= _clock)
            {
                _movement.Jump(_jump[currentJump].Value);
                currentJump++;
            }
            if (currentRotate < _rotation.Count && _rotation[currentRotate].Time <= _clock)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, _rotation[currentRotate].Value);
                currentRotate++;
            }
        }
    }
}

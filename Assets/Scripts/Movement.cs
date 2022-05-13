using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 20f;

    [SerializeField]
    private float _jumpMultiplier = 5f;

    [SerializeField]
    private float _minJump = 1f;

    [SerializeField]
    private float _maxJump = 5f;

    [SerializeField]
    private float _minDistanceToJumpPoint = 0.1f;

    [SerializeField]
    private float _timeToTravelToPoint=0.5f;

    private float _clock = 0f;

    private float _clockJump = 0f;

    private Vector2 _currentPos;
    private Vector2 _pointToTravle;

    private SaveMovement _save;

    public bool CanMove=true;

    private bool _isJumping = false;

    public bool IsJumping
    {
        get { return _isJumping; }
    }

    private void Awake()
    {
        _save = GetComponent<SaveMovement>();
    }

    private void Start()
    {
        _currentPos = transform.position;
        _pointToTravle = transform.position;
    }

    private void Update()
    {
        _isJumping = ((Vector2)transform.position - _pointToTravle).magnitude - _minDistanceToJumpPoint > 0;
        _clock += Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if(_isJumping)
        {
            _clockJump += Time.deltaTime;
            transform.position = Vector2.Lerp(_currentPos, _pointToTravle,_clockJump/_timeToTravelToPoint);
        }
    }

    public void Rotate(float direction)
    {
        if (CanMove)
        {
            _save.AddRotation(new SaveMovement.Tupel(_clock, transform.eulerAngles.z));
            transform.RotateAround(transform.position, Vector3.back, _rotationSpeed * Time.deltaTime * direction);
        }
    }

   

    public void Jump(float holdTime)
    {
        if (!_isJumping && CanMove)
        {
            
            _save.AddJump(new SaveMovement.Tupel(_clock, holdTime));

            holdTime = holdTime * _jumpMultiplier;
            holdTime = Mathf.Clamp(holdTime, _minJump, _maxJump);

            _clockJump = 0f;
            _pointToTravle = holdTime* transform.up + transform.position;
            _currentPos = transform.position;
        }
    }
}

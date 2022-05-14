using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{

    private UnityEvent _startJumping;


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
    private float _timeToTravelToPoint = 0.5f;

    [SerializeField]
    private Vector2 _boundingMax;


    [SerializeField]
    private Vector2 _boundingMin;

    public float TimeToTravelToPoint
    {
        get { return _timeToTravelToPoint; }
    }

    private float _clock = 0f;

    private float _clockJump = 0f;

    private Vector2 _currentPos;
    private Vector2 _pointToTravle;

    private SaveMovement _save;

    public bool CanMove = true;

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
        if (_isJumping)
        {
            _clockJump += Time.deltaTime;
            transform.position = Vector2.Lerp(_currentPos, _pointToTravle, _clockJump / _timeToTravelToPoint);
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
            if (_startJumping != null)
            {
                _startJumping.Invoke();
            }
            _save.AddJump(new SaveMovement.Tupel(_clock, holdTime));

            var _x_0_1 = Mathf.Clamp(holdTime, 0, 1);
            var _x_inf = holdTime + 2 * Mathf.Epsilon;

            var _base_func = _x_inf + 1 / 3 * _x_inf * _x_inf * _x_inf;
            var _offset = -2 * _x_0_1 * _x_0_1 * (_x_0_1 - 2);

            holdTime = (_base_func + _offset) * _jumpMultiplier;
            holdTime = Mathf.Clamp(holdTime, _minJump, _maxJump);

            _clockJump = 0f;
            _pointToTravle = holdTime * transform.up + transform.position;
            _pointToTravle = new Vector2(Mathf.Clamp(_pointToTravle.x, _boundingMin.x, _boundingMax.x), Mathf.Clamp(_pointToTravle.y, _boundingMin.y, _boundingMax.y));
            _currentPos = transform.position;
        }
    }

    public void SubsribeToStartJumping(UnityAction call)
    {
        if (_startJumping == null)
        {
            _startJumping = new UnityEvent();
        }
        _startJumping.AddListener(call);
    }
    public void DeSubsribeToStartJumping(UnityAction call)
    {
        if (_startJumping == null)
        {
            _startJumping = new UnityEvent();
        }
        _startJumping.RemoveListener(call);
    }
}

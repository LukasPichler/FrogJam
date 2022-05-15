using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private  Transform _endPoint;

    private Vector3 nextPos;
    [SerializeField]
    private float speed=3f;
    private float _clock=0f;

    [SerializeField]
    private float _delay = 0f;

    private bool _isMoving = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        nextPos = _startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        _clock += Time.deltaTime;
        if (_clock >= _delay)
        {
            _animator.SetBool("Start", true);
        }

        Vector3 rotPos = nextPos - transform.position;
        rotPos = rotPos.normalized;
        float rot_z = Mathf.Atan2(rotPos.y, rotPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        if (_isMoving)
        {
           

            if (transform.position == _startPoint.position)
            {
                nextPos = _endPoint.position;
                _isMoving = false;
            }

            if (transform.position == _endPoint.position)
            {
                nextPos = _startPoint.position;
                _isMoving = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    public void SetMovingTrue()
    {
        _isMoving = true;
    }
    public void SetMovingFalse()
    {
        _isMoving = false;
    }
}

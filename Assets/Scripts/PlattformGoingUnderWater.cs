using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformGoingUnderWater : MonoBehaviour
{

    private Animator _animator;

    [SerializeField]
    private bool isUnderWater = false;

    [SerializeField]
    private float _timeUnderWater = 2f;

    [SerializeField]
    private float _timeOverWater = 2f;

    [SerializeField]
    private float _startDelay = 0f;

    private float _clock=0f;

    [SerializeField]
    private Collider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _clock -= _startDelay;
        _animator.SetBool("GoUnderWater", isUnderWater);
    }

    // Update is called once per frame
    void Update()
    {
        _clock += Time.deltaTime;
        if (isUnderWater)
        {
            if (_clock >= _timeUnderWater)
            {
                isUnderWater = false;
                _clock = 0f;
                _animator.SetBool("GoUnderWater", isUnderWater);
            }
        }
        else
        {
            if (_clock >= _timeOverWater)
            {
                isUnderWater = true;
                _clock = 0f;
                _animator.SetBool("GoUnderWater", isUnderWater);
            }
        }
    }
    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }
}

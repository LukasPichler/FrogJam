using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _scaleWhileJumping;

    private Animator _animator;

    private Movement _movement;

    private PlayerInputMovement _playerInput;

    private PlaySavings _playSavings;

    private CollisionCheck _collisionCheck;
    

    private float _clock = 0f;
    

    private void Awake()
    {
        _collisionCheck = GetComponent<CollisionCheck>();
        _playSavings = GetComponent<PlaySavings>();
        _playerInput = GetComponent<PlayerInputMovement>();
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("IsJumping",_movement.IsJumping);
        _animator.SetBool("IsInWater", _collisionCheck.IsInWater);
        if (_playerInput != null)
        {
            _animator.SetBool("IsHolding", _playerInput.IsHolding);
        } else if(_playSavings != null)
        {
            _animator.SetBool("IsHolding", _playSavings.IsHolding);
        }
        _animator.SetBool("IsDead", _collisionCheck.IsDead);
        if (_collisionCheck.IsDead)
        {
            float time = _animator.GetCurrentAnimatorStateInfo(0).length;
            
        }


        if (_movement.IsJumping)
        {
            _clock += Time.deltaTime;
            float scale = _scaleWhileJumping.Evaluate(_clock/_movement.TimeToTravelToPoint);
            transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        }
        else
        {
            transform.localScale = Vector3.one;
            _clock = 0f;
        }

    }

   
}

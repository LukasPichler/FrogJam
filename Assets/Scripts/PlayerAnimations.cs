using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    private Movement _movement;

    private PlayerInputMovement _playerInput;

    private PlaySavings _playSavings;

    private CollisionCheck _collisionCheck;
    

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
        

    }

   
}

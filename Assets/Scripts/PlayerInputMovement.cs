using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : MonoBehaviour
{
   

    private float _holdJump = 0f;
    private PlayerInputAction _playerInputAction;
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();
    }


    private void Update()
    {
        if (_playerInputAction.Player.Jump.ReadValue<float>()>0 && !_movement.IsJumping)
        {
            _holdJump += Time.deltaTime;
        }
        else if(_holdJump>0f)
        {
            _movement.Jump(_holdJump);
            _holdJump = 0f;
        }
        if (!_movement.IsJumping)
        {
            float direction = _playerInputAction.Player.Rotate.ReadValue<float>();
            if(direction != 0)
            {
                _movement.Rotate(direction);
            }
            else
            { 
                Vector2 controllerVector = _playerInputAction.Player.RotateControler.ReadValue<Vector2>();
                if (controllerVector == Vector2.zero)
                {

                }
                else
                {
                    float directionController = Vector3.SignedAngle(controllerVector, transform.up, Vector3.forward);

                    float controllDirection = directionController > 0 ? 1 : -1;
                    _movement.Rotate(controllDirection);
                }
            }
        }

    }

 



}

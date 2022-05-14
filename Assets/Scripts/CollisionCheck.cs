using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField]
    private float _radiusBad;
    [SerializeField]
    private float _radiusGood;
    [SerializeField]
    private LayerMask otherFrogs;
    [SerializeField]
    private LayerMask goal;

    [SerializeField]
    private LayerMask ground;

    private ReplayAfterTime _replay;

    public bool IsDead=false;

    public bool IsInWater = false;

    public bool CheckForWaterDeath = false;

    private Movement _movement;

    private bool _inGoal=false;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _replay = GameObject.Find("Timer").GetComponent<ReplayAfterTime>();
    }

    private void FixedUpdate()
    {

        if (Physics2D.CircleCast(transform.position, _radiusBad, Vector3.forward, Mathf.Infinity, otherFrogs))
        {
            Death();
        }

        

        if(!_inGoal && !_movement.IsJumping && Physics2D.CircleCast(transform.position, _radiusGood, Vector3.forward, Mathf.Infinity, goal))
        {
            _inGoal = true;
            _movement.CanMove = false;
            _replay.FrogInGoal();
        }

        if(CheckForWaterDeath && !_movement.IsJumping && !Physics2D.CircleCast(transform.position, _radiusGood, Vector3.forward, Mathf.Infinity, ground))
        {
            IsInWater = true;
            Death();
        }
    }

    private void Death()
    {
        IsDead = true;
        _movement.CanMove = false;
    }

    public void RestartGame()
    {
        
        _replay.ReloadSceneNoSave();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _radiusBad);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _radiusGood);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCheck : MonoBehaviour
{


    private UnityEvent _landingInWater;
    private UnityEvent _poision;

    [SerializeField]
    private float _radiusBad;
    [SerializeField]
    private float _radiusGood;
    [SerializeField]
    private LayerMask otherFrogs;
    [SerializeField]
    private LayerMask goal;
    [SerializeField]
    private LayerMask movingPlatform;

    [SerializeField]
    private LayerMask ground;

    private ReplayAfterTime _replay;

    public bool IsDead=false;

    public bool IsInWater = false;

    public bool CheckForWaterDeath = false;

    private Movement _movement;

    private bool _inGoal=false;

    private Transform _oldParent;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _replay = GameObject.Find("Timer")?.GetComponent<ReplayAfterTime>();
        _oldParent = transform.parent;
    }

    private void Update()
    {
        if (!IsDead)
        {
            if(_replay?.TimeUntilReplay - _replay?.Clock <= 0)
            {
                _movement.CanMove = false;
            }

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _radiusBad, Vector3.forward, Mathf.Infinity, otherFrogs);
            if (hit)
            {
                bool otherFrogJump = hit.transform.gameObject.GetComponent<Movement>().IsJumping;
                if ((otherFrogJump && _movement.IsJumping) || (!otherFrogJump && !_movement.IsJumping))
                {
                    if (_poision != null)
                    {
                        _poision.Invoke();
                    }
                    Death();
                }

                
            }



            if (!_inGoal && !_movement.IsJumping && Physics2D.CircleCast(transform.position, _radiusGood, Vector3.forward, Mathf.Infinity, goal))
            {
                _inGoal = true;
                _movement.CanMove = false;
                _replay.FrogInGoal();
            }

            if (CheckForWaterDeath && !_movement.IsJumping && !Physics2D.CircleCast(transform.position, _radiusGood, Vector3.forward, Mathf.Infinity, ground))
            {
                IsInWater = true;
                Death();
                if (_landingInWater != null)
                {
                    _landingInWater.Invoke();
                }
            }
            RaycastHit2D hitOfPlatform = Physics2D.CircleCast(transform.position, _radiusGood, Vector3.forward, Mathf.Infinity, movingPlatform);
            if (hitOfPlatform && !_movement.IsJumping)
            {
                transform.parent = hitOfPlatform.transform;
                _movement._pointToTravle = transform.position;
            }
            else
            {
                transform.parent = _oldParent;
            }
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

    public void SubscribeToLandingInWater(UnityAction call)
    {
        if (_landingInWater == null)
        {
            _landingInWater = new UnityEvent();
        }
        _landingInWater.AddListener(call);
    }

    public void DeSubscribeToLandingInWater(UnityAction call)
    {
        if (_landingInWater == null)
        {
            _landingInWater = new UnityEvent();
        }
        _landingInWater.RemoveListener(call);
    }



    public void SubscribeToPoison(UnityAction call)
    {
        if (_poision == null)
        {
            _poision = new UnityEvent();
        }
        _poision.AddListener(call);
    }

    public void DeSubscribeToPoison(UnityAction call)
    {
        if (_poision == null)
        {
            _poision = new UnityEvent();
        }
        _poision.RemoveListener(call);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _radiusBad);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _radiusGood);
    }
}

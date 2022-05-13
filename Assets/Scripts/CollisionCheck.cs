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

    private ReplayAfterTime _replay;

    private Movement _movement;

    private bool _inGoal=false;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _replay = GameObject.Find("Timer").GetComponent<ReplayAfterTime>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D[] frograycastHit2D = Physics2D.CircleCastAll(transform.position, _radiusBad, Vector3.forward, Mathf.Infinity, otherFrogs);

        if (frograycastHit2D.Length > 1)
        {
            _movement.CanMove = false;
            _replay.ReloadSceneNoSave();
        }

        

        if(!_inGoal && Physics2D.CircleCast(transform.position, _radiusGood, Vector3.forward, Mathf.Infinity, goal))
        {
            _inGoal = true;
            _movement.CanMove = false;
            _replay.FrogInGoal();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _radiusBad);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _radiusGood);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    private Vector3 nextPos;
    void Start()
    {
        nextPos = startPos.position;
    }

    void FixedUpdate()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position=Vector3.MoveTowards(transform.position,nextPos, speed*Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position,pos2.position);
    }
}

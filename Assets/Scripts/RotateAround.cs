using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 20f;

   

    private void FixedUpdate()
    {

        transform.RotateAround(transform.position, Vector3.back, _rotationSpeed * Time.deltaTime);
    }
}


using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyDamageDeath : MonoBehaviour
{
    public float fullDissolve;
    public float noDissolve;
    public float timeToDissolve = 3f;

    private Color _currentColor;
    [SerializeField]
    private Material _desolveMaterial;

    [SerializeField]
    private SkinnedMeshRenderer _renderer;


    private float _currentHealth;
    private bool _isDead = false;
    private float _clock=0f;

    //TODO shader/material to play


    private void Update()
    {
        if (_isDead)
        {
            if (_clock == 0)
            {
                _renderer.material = _desolveMaterial;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            _clock += Time.deltaTime;
            _renderer.material.SetColor("_EdgeColor",_currentColor);
            _renderer.material.SetFloat("_CutoffHeight",Mathf.Lerp(fullDissolve, noDissolve, _clock/timeToDissolve));
            if (_clock / timeToDissolve >= 1)
            {
                Destroy(gameObject);
            }
            //TODO or deathscream here
        }
    }


   


   
    
}

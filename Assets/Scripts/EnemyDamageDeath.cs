
using System;
using System.Collections;
using System.Security.Cryptography;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class EnemyDamageDeath : MonoBehaviour
{
    public float health;
    public float fireDmg;
    public float iceDmg;
    public float electroDmg;
    public float fullDissolve;
    public float noDissolve;
    public Color fireColor;
    public Color iceColor;
    public Color electroColor;
    public float timeToDissolve = 3f;

    private Color _currentColor;
    [SerializeField]
    private Material _desolveMaterial;

    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks _damageFeedback;
    [SerializeField]
    private MoreMountains.Feedbacks.MMFeedbacks _deathFeedback;

    private MoreMountains.Tools.MMHealthBar _healthBar;
    private float _currentHealth;
    private bool _isDead = false;
    private float _clock=0f;

    //TODO shader/material to play

    private void Awake()
    {
        _healthBar = GetComponent<MoreMountains.Tools.MMHealthBar>();
    }

    void Start()
    {
        _currentHealth = health;
    }

    private void Update()
    {
        if (_isDead)
        {
            if (_clock == 0)
            {
                _renderer.material = _desolveMaterial;
                _deathFeedback.PlayFeedbacks();
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


    public void enemyHit(GameObject projectile)
    {
        if (!_isDead)
        {
            float healBefore = _currentHealth;
            damageEnemy(projectile.tag);
            if (_healthBar != null)
            {
                _healthBar.UpdateBar(_currentHealth, 0f, health, true);
            }
            if (_currentHealth <= 0)
            {
                enemyDeath(projectile.tag);
            }
            else if(healBefore!= _currentHealth)
            {

                _damageFeedback.PlayFeedbacks();
                StartCoroutine(FreezeForSeconds(1f));
            }
        }

       
    }


    private IEnumerator FreezeForSeconds(float sec)
    {

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(sec);
        if (!_isDead)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    private void damageEnemy(string projectileTag)
    {
        if (projectileTag. Equals("FireProj"))
        {
            _currentHealth -= fireDmg;
        }
        if (projectileTag.Equals("IceProj"))
        {
            _currentHealth -= iceDmg;
        }
        if (projectileTag.Equals("ElectroProj"))
        {
            _currentHealth -= electroDmg;
        }
    }

    private void enemyDeath(string tag)
    {
        _isDead = true;
        if (tag.Equals("FireProj"))
        {
            _currentColor = fireColor;
        }
        if (tag.Equals("IceProj"))
        {
            _currentColor =iceColor;
        }
        if (tag.Equals("ElectroProj"))
        {
            _currentColor =electroColor;
        }
        
    }
    
}

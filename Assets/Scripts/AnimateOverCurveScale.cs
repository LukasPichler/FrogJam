using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOverCurveScale : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _curve;

    [SerializeField]
    private float _cicleTime;

    private float _clock=0f;

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _clock += Time.deltaTime;
        if (_clock > _cicleTime)
        {
            _clock = 0f;
        }
        transform.localScale = baseScale * _curve.Evaluate(_clock/_cicleTime);
    }
}

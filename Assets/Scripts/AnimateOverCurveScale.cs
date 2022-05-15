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

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        baseScale = _rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _clock += Time.deltaTime;
        if (_clock > _cicleTime)
        {
            _clock = 0f;
        }
        _rectTransform.localScale = baseScale * _curve.Evaluate(_clock/_cicleTime);
    }
}

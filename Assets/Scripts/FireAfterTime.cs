using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAfterTime : MonoBehaviour
{
    private ReplayAfterTime _replay;
    [SerializeField]
    private AnimationCurve _volume;

    private Transform _fire;

    [SerializeField]
    private Transform _burnPoint;

    private float _clock = 0f;

    [SerializeField]
    private AudioSource _audio;

    private bool _onFire = false;

    private Vector3 _fireCurrentPos;

    [SerializeField]
    private AnimationCurve _movementToCenter;


    [SerializeField]
    private AnimationCurve _scaleToCenter;

    private void Awake()
    {
        _replay = GetComponent<ReplayAfterTime>();
        _fire = _replay.Fire;
    }

    private void OnEnable()
    {
        _replay.SubscribeToReloadUnsaved(FireStuff);
    }

    private void OnDisable()
    {
        _replay.DeSubscribeToReloadUnsaved(FireStuff);
    }


    // Update is called once per frame
    void Update()
    {
        float volume = _volume.Evaluate(_replay.Clock / _replay.TimeUntilReplay);
        _audio.volume = volume;

        if (_onFire)
        {
            _clock += Time.deltaTime;
            _fire.position = Vector3.Lerp(_fireCurrentPos, _burnPoint.position, _movementToCenter.Evaluate(_clock));
            _fire.localScale = new Vector3(_scaleToCenter.Evaluate(_clock), _scaleToCenter.Evaluate(_clock), 1);
        }
    }


    private void FireStuff()
    {
        _onFire = true;
        _fireCurrentPos = _fire.position;
        _fire.GetComponent<AnimateOverCurveScale>().enabled = false;
    }
}

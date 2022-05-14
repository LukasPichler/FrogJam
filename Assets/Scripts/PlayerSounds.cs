using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private bool _isOtherFrog = false;

    [SerializeField]
    private float _otherFrogVolumeScale = 0.5f;

    [SerializeField]
    private List<AudioClip> _jumpingSounds = new List<AudioClip>();
    [SerializeField]
    private float _minVolumeJumping = 0.5f;
    [SerializeField]
    private float _maxVolumeJumping = 1f;
    [SerializeField]
    private float _minPitchJumping = 0.7f;
    [SerializeField]
    private float _maxPitchJumping = 1f;

    [SerializeField]
    private List<AudioClip> _waterJump = new List<AudioClip>();
    [SerializeField]
    private float _minVolumeWater = 0.5f;
    [SerializeField]
    private float _maxVolumeWater = 1f;
    [SerializeField]
    private float _minPitchWater = 0.7f;
    [SerializeField]
    private float _maxPitchWater = 1f;

    [SerializeField]
    private List<AudioClip> _poison = new List<AudioClip>();
    [SerializeField]
    private float _minVolumePoison = 0.5f;
    [SerializeField]
    private float _maxVolumePoison = 1f;
    [SerializeField]
    private float _minPitchPoison = 0.7f;
    [SerializeField]
    private float _maxPitchPoison = 1f;

    private Movement _movement;

    private CollisionCheck _collisionCheck;

    private AudioSource _audioSource;

    private void Awake()
    {
        _collisionCheck = GetComponent<CollisionCheck>();
        _movement = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _movement.SubsribeToStartJumping(PlayJumpingSound);
        _collisionCheck.SubscribeToLandingInWater(PlayWaterJumpSound);
        _collisionCheck.SubscribeToPoison(PlayerPoisonSound);
    }

    private void OnDisable()
    {

        _movement.DeSubsribeToStartJumping(PlayJumpingSound);
        _collisionCheck.DeSubscribeToLandingInWater(PlayWaterJumpSound);
        _collisionCheck.DeSubscribeToPoison(PlayerPoisonSound);
    }

    private void PlayJumpingSound()
    {
        _audioSource.clip = _jumpingSounds[Random.Range(0, _jumpingSounds.Count)];
        _audioSource.volume = Random.Range(_minVolumeJumping, _maxVolumeJumping);
        if (_isOtherFrog)
        {
            _audioSource.volume *= _otherFrogVolumeScale;
        }
        _audioSource.pitch = Random.Range(_minPitchJumping, _maxPitchJumping);
        _audioSource.Play();
    }

    private void PlayWaterJumpSound()
    {
        _audioSource.clip = _waterJump[Random.Range(0, _waterJump.Count)];
        _audioSource.volume = Random.Range(_minVolumeWater, _maxVolumeWater);
        if (_isOtherFrog)
        {
            _audioSource.volume *= _otherFrogVolumeScale;
        }
        _audioSource.pitch = Random.Range(_minPitchWater, _maxPitchWater);
        _audioSource.Play();
    }

    private void PlayerPoisonSound()
    {
        _audioSource.clip = _poison[Random.Range(0, _poison.Count)];
        _audioSource.volume = Random.Range(_minVolumePoison, _maxVolumePoison);
        if (_isOtherFrog)
        {
            _audioSource.volume *= _otherFrogVolumeScale;
        }
        _audioSource.pitch = Random.Range(_minPitchPoison, _maxPitchPoison);
        _audioSource.Play();
    }

}

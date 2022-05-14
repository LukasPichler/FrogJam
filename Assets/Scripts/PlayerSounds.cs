using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _jumpingSounds = new List<AudioClip>();
    [SerializeField]
    private float _minVolumeJumping=0.5f;
    [SerializeField]
    private float _maxVolumeJumping = 1f;
    [SerializeField]
    private float _minPitchJumping = 0.7f;
    [SerializeField]
    private float _maxPitchJumping = 1f;

    private Movement _movement;

    private AudioSource _audioSource;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _movement.SubsribeToStartJumping(PlayJumpingSound);
    }

    private void OnDisable()
    {

        _movement.DeSubsribeToStartJumping(PlayJumpingSound);
    }

    public void PlayJumpingSound()
    {
        _audioSource.clip = _jumpingSounds[Random.Range(0,_jumpingSounds.Count)];
        _audioSource.volume = Random.Range(_minVolumeJumping,_maxVolumeJumping);
        _audioSource.pitch = Random.Range(_minPitchJumping,_maxPitchJumping);
        _audioSource.Play();
    }
}

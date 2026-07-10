using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _mainAudioSource;
    [SerializeField] private AudioSource _cannonAuidoSource;
    [SerializeField] private AudioClip _cannonFireSound;
    [SerializeField] private AudioClip _shipMovementSound;
    [SerializeField] private AudioClip _hitSound;

    [SerializeField][Range(0f, 1f)] private float _maxMovementVolume = 1f;

    public void PlayCannonFireSoundEffect()
    {
        _cannonAuidoSource.PlayOneShot(_cannonFireSound);
    }
    public void PlayHitSoundEffect()
    {
        _cannonAuidoSource.PlayOneShot(_hitSound);
    }

    public void UpdateMovementSound(float speedRatio)
    {
        if (speedRatio > 0.01f)
        {
            if (!_mainAudioSource.isPlaying || _mainAudioSource.clip != _shipMovementSound)
            {
                _mainAudioSource.clip = _shipMovementSound;
                _mainAudioSource.loop = true;
                _mainAudioSource.Play();
            }


            _mainAudioSource.volume = speedRatio * _maxMovementVolume;
        }
        else
        {
            if (_mainAudioSource.isPlaying && _mainAudioSource.clip == _shipMovementSound)
            {
                _mainAudioSource.volume = 0f;
                _mainAudioSource.Stop();
            }   
        }
    }
}
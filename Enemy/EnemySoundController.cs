using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioSource _cannonAudioSource;
    [SerializeField] private AudioClip _cannonFireSound;
    [SerializeField] private AudioClip _hitSound;



    public void PlayCannonSoundEffect()
    {
        _cannonAudioSource.PlayOneShot(_cannonFireSound);
    }
    public void PlayHitSoundEffect() 
    {
        _cannonAudioSource.PlayOneShot(_hitSound);
    }
}

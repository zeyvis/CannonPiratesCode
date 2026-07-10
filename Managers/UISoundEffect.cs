using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip _UIClip;
    [SerializeField] private AudioClip _UIbackClip;
    [SerializeField] private AudioClip _UIUnlockClip;
    [SerializeField] private AudioClip _UIBuyClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    public void PlayUISoundEffect()
    {
        _audioSource.PlayOneShot(_UIClip);
    }
    public void PlayBackSoundEffect()
    {
        _audioSource.PlayOneShot(_UIbackClip);
    }
    public void PlayUnlockSoundEffect()
    {
        _audioSource.PlayOneShot(_UIUnlockClip);
    }
    public void PlayBuySoundEffect()
    {
        _audioSource.PlayOneShot(_UIBuyClip);
    }

}

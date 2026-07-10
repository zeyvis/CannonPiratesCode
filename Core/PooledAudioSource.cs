using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : MonoBehaviour
{
    private AudioSource _audioSource;
    private Action<PooledAudioSource> _returnToPoolAction;
    private Coroutine _soundCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();


        _audioSource.spatialBlend = 1.0f;
        _audioSource.playOnAwake = false;


        _audioSource.dopplerLevel = 0f;
    }

    public void Play(AudioClip clip, Vector3 position, float volume, float minDistance, float maxDistance, Action<PooledAudioSource> returnAction)
    {

        transform.position = position;
        _audioSource.clip = clip;
        _audioSource.volume = volume;


        _audioSource.minDistance = minDistance;
        _audioSource.maxDistance = maxDistance;
        _returnToPoolAction = returnAction;

        gameObject.SetActive(true);
        _audioSource.Play();

        if (_soundCoroutine != null) StopCoroutine(_soundCoroutine);
        _soundCoroutine = StartCoroutine(WaitForSoundEnd(clip.length));
    }

    private IEnumerator WaitForSoundEnd(float duration)
    {
        yield return new WaitForSeconds(duration);

        _audioSource.Stop();
        gameObject.SetActive(false);

        _returnToPoolAction?.Invoke(this);
    }
}
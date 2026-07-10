using System.Collections.Generic;
using UnityEngine;

public class AudioPoolManager : MonoBehaviour
{
    public static AudioPoolManager Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private int initialPoolSize = 5;

    private Queue<PooledAudioSource> _poolQueue = new Queue<PooledAudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewAudioSourceInstance();
        }
    }

    private PooledAudioSource CreateNewAudioSourceInstance()
    {
        GameObject go = new GameObject("PooledAudioSource");
        go.transform.SetParent(transform);

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.rolloffMode = AudioRolloffMode.Linear;

        PooledAudioSource pooledAudio = go.AddComponent<PooledAudioSource>();

        go.SetActive(false);
        _poolQueue.Enqueue(pooledAudio);

        return pooledAudio;
    }


    public void PlaySound3D(AudioClip clip, Vector3 position, float volume = 1.0f, float minDistance = 1f, float maxDistance = 20f)
    {
        if (clip == null) return;

        PooledAudioSource audioSourceInstance;

        if (_poolQueue.Count > 0)
        {
            audioSourceInstance = _poolQueue.Dequeue();
        }
        else
        {
            audioSourceInstance = CreateNewAudioSourceInstance();
        }

        audioSourceInstance.Play(clip, position, volume, minDistance, maxDistance, ReturnToPool);
    }

    private void ReturnToPool(PooledAudioSource audioSourceInstance)
    {
        _poolQueue.Enqueue(audioSourceInstance);
    }
}
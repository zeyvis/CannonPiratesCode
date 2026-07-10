using System.Collections.Generic;
using UnityEngine;

public class HitParticlePool : MonoBehaviour
{
    public static HitParticlePool Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private int _poolSize = 10; 

    private List<ParticleSystem> _pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _pool = new List<ParticleSystem>();
        for (int i = 0; i < _poolSize; i++)
        {
            ParticleSystem ps = Instantiate(_particlePrefab, transform);
            ps.gameObject.SetActive(false); 
            _pool.Add(ps);
        }
    }

    public void PlayParticle(Vector3 position)
    {
        foreach (var ps in _pool)
        {
            if (!ps.gameObject.activeInHierarchy)
            {
                ps.transform.position = position;
                ps.gameObject.SetActive(true);
                ps.Play();
                return; 
            }
        }

       
    }
}

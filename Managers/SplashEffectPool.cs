using UnityEngine;
using System.Collections.Generic;

public class SplashEffectPool : MonoBehaviour
{
    public static SplashEffectPool Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private GameObject _splashPrefab;
    [SerializeField] private int _poolSize = 5;

    private List<GameObject> _pool = new List<GameObject>();

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
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_splashPrefab, transform);
            obj.SetActive(false);
            _pool.Add(obj);
        }
    }

    public GameObject GetSplashEffect(Vector3 position, Quaternion rotation)
    {
        foreach (var splash in _pool)
        {
            if (!splash.activeInHierarchy)
            {
                splash.transform.position = position;
                splash.transform.rotation = rotation; 
                splash.SetActive(true);
                return splash;
            }
        }

        GameObject newSplash = Instantiate(_splashPrefab, transform);
        newSplash.transform.position = position;
        newSplash.transform.rotation = rotation;
        _pool.Add(newSplash);

        return newSplash;
    }
}
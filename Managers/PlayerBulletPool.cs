using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private int _poolSize = 3;


    private float _bulletDamage;
    private List<CannonBall> _pool = new List<CannonBall>();

    public void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            
            GameObject obj = Instantiate(_ballPrefab, this.transform);
            obj.SetActive(false);
            _pool.Add(obj.GetComponent<CannonBall>());
        }
    }

    public CannonBall GetBallFromPool()
    {
        foreach (var ball in _pool)
        {
            if (!ball.gameObject.activeInHierarchy)
            {
                ball.SetDamage(_bulletDamage);
                return ball;
            }
        }

        return _pool[0];
    }

    public void SetBulletDamage(float damage)
    {
        _bulletDamage = damage;
    }

    public void OnEnable()
    {
        EntryPanel.onStartGame += InitializePool;
    }
    public void OnDisable()
    {
        EntryPanel.onStartGame -= InitializePool;
    }
}
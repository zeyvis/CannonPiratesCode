using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool Instance { get; private set; }

    [Header("Pool Settings")]
    [SerializeField] private EnemyBullet _bulletPrefab;
    [SerializeField] private int _poolSize = 30; 

    private Queue<EnemyBullet> _bulletPool = new Queue<EnemyBullet>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            EnemyBullet newBullet = Instantiate(_bulletPrefab, transform);
            newBullet.gameObject.SetActive(false); 
            _bulletPool.Enqueue(newBullet);
        }
    }

    public EnemyBullet GetBullet(Vector3 position, Quaternion rotation, float damageAmount)
    {
        if (_bulletPool.Count > 0)
        {
            EnemyBullet bullet = _bulletPool.Dequeue();
            bullet.damage = damageAmount;
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        EnemyBullet extraBullet = Instantiate(_bulletPrefab, position, rotation);
        return extraBullet;
    }

    public void ReturnToPool(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : MonoBehaviour
{
    public float damage = 0;

    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private AudioClip _splashAudioClip;
    private Rigidbody _rb;
    private TrailRenderer _trail; 
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
       
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        if (_trail != null)
        {
            _trail.Clear();
            _trail.emitting = true;
        }

        StartCoroutine(LifeTimeRoutine());
    }

    private void OnDisable()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        if (_trail != null)
        {
            _trail.emitting = false;
        }
    }

    private IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        ReturnToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageableTarget) && other.CompareTag("Player"))
        {
            damageableTarget.TakeDamage(damage);
            ReturnToPool();
        }
        else if (other.CompareTag("Sea"))
        {
            if (SplashEffectPool.Instance != null)
            {
                SplashEffectPool.Instance.GetSplashEffect(transform.position, Quaternion.identity);
            }

            Vector3 hitPosition = transform.position;

            AudioPoolManager.Instance.PlaySound3D(_splashAudioClip, hitPosition, 1.0f, 3f, 35f);

            if (_trail != null) _trail.emitting = false;

            StartCoroutine(DeactiveAfterDelay(1.5f));
        }
    }

    private IEnumerator DeactiveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        StopAllCoroutines();
        if (_trail != null) _trail.emitting = false;

        EnemyBulletPool.Instance.ReturnToPool(this);
    }
}
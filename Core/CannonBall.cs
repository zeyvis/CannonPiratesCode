using System;
using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 100f;
    [SerializeField] private AudioClip _splashAudioClip;
    private Rigidbody _rb;
    private TrailRenderer _trailRenderer;
    private CameraController _cameraController;

   
    private float _airTime = 0f;

   
    private bool _isFlying = false;
    private Vector3 _startPosition;

    public static event Action<float> OnBallHitTarget;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (_trailRenderer == null)
            _trailRenderer = GetComponentInChildren<TrailRenderer>();

        if (_cameraController == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                _cameraController = player.GetComponent<CameraController>();
        }
    }

   
    private void Update()
    {
        
        if (_isFlying)
        {
            _airTime += Time.deltaTime;
        }
    }

    public void Launch(Vector3 force)
    {
        _isFlying = true;
        _airTime = 0f;

       
        _startPosition = transform.position;

        if (_cameraController != null)
            _cameraController.BallLaunched();

        if (_trailRenderer != null)
        {
            _trailRenderer.Clear();
            _trailRenderer.emitting = true;
        }

        _rb.isKinematic = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.AddForce(force, ForceMode.Impulse);
    }

    public void SetDamage(float damage)
    {
        _damageAmount = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageableTarget))
        {
            OnLastHit(damageableTarget.GetHealth());
            damageableTarget.TakeDamage(_damageAmount);
               
            

            HandleHit(); 
            DeactivateBall();
        }
        else if (other.CompareTag("Sea"))
        {
            if (SplashEffectPool.Instance != null)
            {
                SplashEffectPool.Instance.GetSplashEffect(transform.position, Quaternion.identity);
            }
            Vector3 hitPosition = transform.position;

            
            AudioPoolManager.Instance.PlaySound3D(_splashAudioClip, hitPosition, 1.0f, 3f, 35f);

            if (_trailRenderer != null) _trailRenderer.emitting = false;

            HandleHit(); 
            StartCoroutine(DeactivateAfterDelay(1.5f));
        }
    }

    
    private void HandleHit()
    {
        if (_isFlying)
        {
            _isFlying = false;

            if (_cameraController != null)
                _cameraController.BallHit();
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DeactivateBall();
    }

    private void DeactivateBall()
    {
        if (_trailRenderer != null) _trailRenderer.emitting = false;
        gameObject.SetActive(false);
    }
    private void OnLastHit(float health)
    {
        if (_damageAmount>=health)
        {
            float traveledDistance = Vector3.Distance(_startPosition, transform.position);
            OnBallHitTarget?.Invoke(traveledDistance);
        }
    }

    private void OnDisable()
    {
        if (_rb) _rb.isKinematic = true;
        StopAllCoroutines();

        
        HandleHit();
    }
}
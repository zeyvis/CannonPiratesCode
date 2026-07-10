using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private BoxCollider _mainCollider;
    [Tooltip("Ne kadar sürede batsýn(arttýrýrsan hýzlý batar)")]
    [SerializeField] private float _sinkTimer = 2f;
    [Tooltip("Nereye kadar batsýn")]
    [SerializeField] private float _targerSinkPosY = -0.7f;

    [Tooltip("To update the ship parts' health")]
    [SerializeField] private PlayerShipPartsController[] _shipPartsControllers;

    public static event Action OnPlayerDeath;

    private bool _isDead = false;

    public void TakeDamage(float damageAmount)
    {
        Vector3 particlePosition = transform.position + Vector3.up;

        HitParticlePool.Instance.PlayParticle(particlePosition);

        _health -= damageAmount;
    }
    public float GetHealth()
    {
        return _health;
    }

    public void SetShipPartsHealth(float shipPartsHealth)
    {
        foreach (PlayerShipPartsController shippart in _shipPartsControllers) 
        {
            shippart.SetHealth(shipPartsHealth);
        }
    }

   
    public void Die()
    {
        if (_isDead) return;
        _isDead = true;
        StartCoroutine(SinkShipRoutine());
    }

    private IEnumerator SinkShipRoutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, _targerSinkPosY, startPosition.z);

        float duration = _sinkTimer;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;


        OnPlayerDeath?.Invoke();

        
    }
    public void ReviveFullShip()
    {
        _isDead = false;
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        foreach (PlayerShipPartsController part in _shipPartsControllers)
        {
            if (part != null)
            {
                part.RevivePart();
            }
        }

    }
    public void OnEnable()
    {
        ContinueButton.OnContinueGame += ReviveFullShip;
    }
    public void OnDisable()
    {
        ContinueButton.OnContinueGame -= ReviveFullShip;
    }
}
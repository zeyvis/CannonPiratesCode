using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsController : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private GameObject _spareParts;
    [SerializeField] private EnemyShipHealth _enemyShipHealth;
    [SerializeField] private EnemySoundController _enemySoundController;

    private float _healthBrokeBorder = 0f;

    private void Start()
    {
        _healthBrokeBorder = _health / 2;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HitTheShip();
        _enemySoundController.PlayHitSoundEffect();
    }
    public float GetHealth()
    {
        return _health;
    }


    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void HitTheShip()
    {
        
        Vector3 particlePosition = transform.position + Vector3.up;

        
        HitParticlePool.Instance.PlayParticle(particlePosition);

        if (_health < _healthBrokeBorder && _health > 0)
        {
            _spareParts.SetActive(false);
            MakeMaterialRed();
        }
        else if (_health <= 0)
        {
            Die();
            _enemyShipHealth.Die();
        }
    }

   
    public void MakeMaterialRed()
    {
        Renderer objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            objRenderer.material.color = Color.red;
        }
        else
        {
            Debug.LogWarning("Bu objede Renderer bileþeni bulunamadý!");
        }
    }
}
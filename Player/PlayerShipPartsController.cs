using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipPartsController : MonoBehaviour, IDamageable
{
    public enum Positions
    {
        Front,
        Mid,
        Back
    }

    [SerializeField] private float _health = 100f;
    [SerializeField] private GameObject _spareParts;
    [SerializeField] private ShipHealth _shipHealth;
    [SerializeField] private Positions _selectedPosition;
    [SerializeField] private PlayerSoundEffect _playerSoundEffect;

    private int _selectedPositionIndex;
    private float _baseHealth;

    void Start()
    {
        _selectedPositionIndex = (int)_selectedPosition;
        _baseHealth = _health;
    }

  
    public void TakeDamage(float damageAmount)
    {
        _health-=damageAmount;
        HitTheShip();
        _playerSoundEffect.PlayHitSoundEffect();
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

       
        if (_health < 50 && _health > 0)
        {
            _spareParts.SetActive(false);
            ShipStatus.Instance.SetShipImage(_selectedPositionIndex, Color.yellow);
        }
        else if (_health <= 0)
        {
            Die();
            ShipStatus.Instance.SetShipImage(_selectedPositionIndex, Color.red);
            _shipHealth.Die();
        }
    }

    public void SetHealth(float health)
    {
        _health = health;
    }
    public void RevivePart() 
    {
        gameObject.SetActive(true); 
        _health = _baseHealth;
        _spareParts.SetActive(true); 

        ShipStatus.Instance.SetShipImage(_selectedPositionIndex, Color.white);
        Debug.Log($"Shipparttan çalýþtý. basehealth: {_baseHealth} , mevcut can: {_health} , konumu:{_selectedPosition}");
    }


}

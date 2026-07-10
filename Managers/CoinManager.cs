using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int _coinAmount;

    private int _collectedCoin;

    private const string COIN_PREF_KEY = "SavedCoinAmount";

    private void Awake()
    {
        _coinAmount = PlayerPrefs.GetInt(COIN_PREF_KEY, 0);
    }

    public void OnEnemyDeath()
    {
        CollectedCoinAmount(10);
    }

    public void CollectedCoinAmount(int earnedCoinAmount)
    {
        _collectedCoin += earnedCoinAmount;     
    }
    
    
    public void ClaimCoin()
    {
        _coinAmount += _collectedCoin;
        SaveCoinAmount();
    }
    
    public int GetCoinAmount()
    {
        return _coinAmount;
    }
    public void SetCoinAmount(int coinAmount)
    {
        _coinAmount += coinAmount;
        SaveCoinAmount();
    }
    public int GetCollectedCoinAmount()
    {
        return _collectedCoin;
    }


    public void SaveCoinAmount()
    {
        PlayerPrefs.SetInt(COIN_PREF_KEY, _coinAmount);
        PlayerPrefs.Save(); 
        _collectedCoin = 0;

    }
    public void OnEnable()
    {
        EnemyShipHealth.OnEnemyDeath += OnEnemyDeath;
    }

    public void OnDisable()
    {
        EnemyShipHealth.OnEnemyDeath -= OnEnemyDeath;
    }

    [ContextMenu("Reset Coin Amount")]
    public void ResetCoinAmount()
    {
        _coinAmount = 0;
        PlayerPrefs.SetInt(COIN_PREF_KEY, 0);
        PlayerPrefs.Save();
        Debug.Log("Para sýfýrlandý. Mevcut para: " + _coinAmount);
    }
    [ContextMenu("add Coin (500)")]
    public void AddCoin()
    {
        _coinAmount += 5000;
        PlayerPrefs.SetInt(COIN_PREF_KEY, 0);
        PlayerPrefs.Save();
        Debug.Log("Para eklendi. Mevcut para: " + _coinAmount);
    }
}
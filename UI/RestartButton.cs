using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private CoinManager _coinManager;
    public void RestartTheGame()
    {
        _coinManager.ClaimCoin();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private EntryPanel _entryPanel;
    public void PlayerDeath()
    {
        _gameOverPanel.SetActive(true);
        _coinText.text=_coinManager.GetCollectedCoinAmount().ToString();
        _scoreText.text=_playerScore.GetScore().ToString(); 
        _highScoreText.text=_playerScore.GetHighScore().ToString();
        _entryPanel.CloseTheGamePlayUI();
    }
    private void ContinueGame()
    {
        _gameOverPanel.SetActive(false);
        _entryPanel.OpenTheGamePlayUI();
    }
    public void OnEnable()
    {
        ShipHealth.OnPlayerDeath += PlayerDeath;
        ContinueButton.OnContinueGame += ContinueGame;
    }
    public void OnDisable()
    {
        ShipHealth.OnPlayerDeath -= PlayerDeath;
        ContinueButton.OnContinueGame -= ContinueGame;

    }
}

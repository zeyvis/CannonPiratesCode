using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _score = 0;

    public void SetScore()
    {
        _score++;
        CheckAndSaveHighScore(); 
    }

    public int GetScore()
    {
        return _score;
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    
    public void CheckAndSaveHighScore()
    {
        
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        
        if (_score > currentHighScore)
        {
           
            PlayerPrefs.SetInt("HighScore", _score);
            PlayerPrefs.Save(); 
        }
    }

    public void OnEnable()
    {
        EnemyShipHealth.OnEnemyDeath += SetScore;
    }

    public void OnDisable()
    {
        EnemyShipHealth.OnEnemyDeath -= SetScore;
        CheckAndSaveHighScore();
    }
    [ContextMenu("Reset High Score")]
    public void ResetHighScore()
    {
        
         PlayerPrefs.SetInt("HighScore", 0);

        PlayerPrefs.Save(); 

        Debug.Log("High Score baþarýyla sýfýrlandý!");
    }
}
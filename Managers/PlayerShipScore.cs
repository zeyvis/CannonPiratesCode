using UnityEngine;

public class PlayerShipScore : MonoBehaviour
{
    private int _currentShipPoint = 0;  
    private int _currentCannonPoint = 0;
    private float _totalScore;

    public void SetShipScore(int shipPoint)
    {
        _currentShipPoint = shipPoint;
        CalculateTotalScore();
    }

    public void SetCannonScore(int cannonPoint)
    {
        _currentCannonPoint = cannonPoint;
        CalculateTotalScore();
    }

    private void CalculateTotalScore()
    {
        _totalScore = (_currentShipPoint + 1) * 1.2f + (_currentCannonPoint + 1) * 1.8f;
        Debug.Log("player shipScore:"+_totalScore);
    }

    public float GetScore()
    {
        return _totalScore;
    }
}
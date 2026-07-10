using UnityEngine;

public enum GameDifficulty
{
    Easy = 1,
    Mid = 2,
    Hard = 3
}

public class DifficultyManager : MonoBehaviour
{
    [Header("Spawner Reference")]
    [SerializeField] private VaweEnemyShipSpawner enemySpawner;

    private GameDifficulty currentDifficulty = GameDifficulty.Easy;


    public void SetDifficulty(float newDifficulty)
    {
        currentDifficulty = (GameDifficulty)Mathf.RoundToInt(newDifficulty);

        ApplySettingsToSpawner();
    }

    public GameDifficulty GetDifficulty()
    {
        return currentDifficulty;
    }


    private void ApplySettingsToSpawner()
    {
        if (enemySpawner == null) return;

        switch (currentDifficulty)
        {
            case GameDifficulty.Easy:
                enemySpawner.SetDifficultyParameters(17f, 2);
                break;
            case GameDifficulty.Mid:
                enemySpawner.SetDifficultyParameters(10f, 2);
                break;
            case GameDifficulty.Hard:
                enemySpawner.SetDifficultyParameters(5f, 4);
                break;
        }
    }
}
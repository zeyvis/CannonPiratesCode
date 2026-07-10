using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Components")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider musicSlider;

    [Header("Difficulty Components")]
    [SerializeField] private DifficultyManager difficultyManager;
    [SerializeField] private Slider difficultySlider;

    private const string MusicVolumeKey = "MusicVolume";
    private const float DefaultVolume = 0.5f;

    private const string DifficultyKey = "GameDifficulty";
    private const float DefaultDifficulty = 2f;

    private void Start()
    {
        InitializeSettings();
    }


    private void InitializeSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultVolume);
        if (musicSource != null) musicSource.volume = savedVolume;
        if (musicSlider != null) musicSlider.value = savedVolume;

        float savedDifficulty = PlayerPrefs.GetFloat(DifficultyKey, DefaultDifficulty);

        if (difficultySlider != null)
        {
            difficultySlider.value = savedDifficulty;
        }

        if (difficultyManager != null)
        {
            difficultyManager.SetDifficulty(savedDifficulty);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }

        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetDifficultyLevel(float difficultyValue)
    {
        if (difficultyManager != null)
        {
            difficultyManager.SetDifficulty(difficultyValue);
        }

        PlayerPrefs.SetFloat(DifficultyKey, difficultyValue);
        PlayerPrefs.Save();
    }

    [ContextMenu("Ayarlarý Varsayýlana Sýfýrla")]
    public void ResetToDefaultSettings()
    {
        if (musicSlider != null)
            musicSlider.value = DefaultVolume;

        if (difficultySlider != null)
            difficultySlider.value = DefaultDifficulty;

        SetMusicVolume(DefaultVolume);
        SetDifficultyLevel(DefaultDifficulty);

        Debug.Log("Ayarlar sýfýrlandý -> Müzik: 0.5 | Zorluk: Mid (2)");
    }
}
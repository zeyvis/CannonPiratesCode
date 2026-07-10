using UnityEngine;
using UnityEngine.UI;

public class FpsManager : MonoBehaviour
{
    [SerializeField] private Toggle powerSavingToggle; 

    private int _deviceRefreshRate;
    private const string PowerSavingKey = "PowerSavingEnabled";

    private void Start()
    {
       
        QualitySettings.vSyncCount = 0;

        _deviceRefreshRate = (int)Mathf.Round((float)Screen.currentResolution.refreshRateRatio.value);

       
        bool isPowerSavingEnabled = PlayerPrefs.GetInt(PowerSavingKey, 0) == 1;

        
        if (powerSavingToggle != null)
        {
            powerSavingToggle.SetIsOnWithoutNotify(isPowerSavingEnabled);
        }

        
        ApplyFpsSettings(isPowerSavingEnabled);
    }

    
    public void TogglePowerSaving(bool isEnabled)
    {
        PlayerPrefs.SetInt(PowerSavingKey, isEnabled ? 1 : 0);
        PlayerPrefs.Save();

        ApplyFpsSettings(isEnabled);
    }

    private void ApplyFpsSettings(bool isPowerSavingEnabled)
    {
        if (isPowerSavingEnabled)
        {
            Application.targetFrameRate = 60;
            Debug.Log("Güç Tasarrufu AÇIK: FPS 60'a sabitlendi.");
        }
        else
        {
            Application.targetFrameRate = _deviceRefreshRate;
            Debug.Log($"Güç Tasarrufu KAPALI: FPS cihazýn yenileme hýzýna ({_deviceRefreshRate}) sabitlendi.");
        }
    }
}
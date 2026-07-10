using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntryPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _gamePlayUI;
    [SerializeField] private GameObject _entryPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _settingsPanel;

    public static event Action onShop; //ShipbobAnimate and CarCamera
    public static event Action onPlay; //ShipbobAnimate and CarCamera
    public static event Action onStartGame;





    public void StartTheGame()
    {
        foreach (GameObject UIObjects  in _gamePlayUI)
        {
            UIObjects.SetActive(true);
        }

        _entryPanel.SetActive(false);
        onStartGame?.Invoke();
    }

    public void CloseTheGamePlayUI()
    {
        foreach (GameObject UIObjects in _gamePlayUI)
        {
            UIObjects.SetActive(false);
        }
    }
    public void OpenTheGamePlayUI()
    {
        foreach (GameObject UIObjects in _gamePlayUI)
        {
            UIObjects.SetActive(true);
        }
    }

    public void Shop()
    {
        onShop?.Invoke();
        _entryPanel.SetActive(false);
        _shopPanel.SetActive(true);
    }
    public void Settings()
    {
        _settingsPanel.SetActive(true);
        _entryPanel.SetActive(false);
        _shopPanel.SetActive(false);
        
    }
    public void BackToEntry()
    {
        _entryPanel.SetActive(true);
        _shopPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        onPlay?.Invoke();
    }

    
  
}

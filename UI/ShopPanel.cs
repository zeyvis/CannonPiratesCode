using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject _shipButtons;
    [SerializeField] private GameObject _cannonButtons;
    [SerializeField] private GameObject _sailButtons;

    [Header("Managers & References")]
    [SerializeField] private ShipSpawner _shipSpawner;
    [SerializeField] private ShipStatsUI _shipStatsUI;      
    [SerializeField] private CannonStatsUI _cannonStatsUI;  
    [SerializeField] private ButtonInactiver _buttonInactiver;
    [SerializeField] private PlayerShipScore _playerShipScore;

    [Header("Databases")]
    [SerializeField] private ShipData[] _shipDatas;
    [SerializeField] private CannonData[] _cannonDatas;
    [SerializeField] private SailData[] _sailDatas;        

    public void SelectShip(int shipIndex)
    {
        Debug.Log("Seçilen Gemi Index: " + shipIndex);

        _playerShipScore.SetShipScore(shipIndex);

        if (_shipSpawner != null)
        {
            _shipSpawner.ChangeShip(shipIndex);
            _buttonInactiver.InactiveSelectButton(ItemCategory.Ship, shipIndex);
        }
        else
        {
            Debug.LogError("ShipSpawner referansý ShopPanel'e atanmamýþ!");
        }

        RefreshShipStatsUI();
    }

    public void SelectCannon(int cannonIndex)
    {
        Debug.Log("Seçilen cannon Index: " + cannonIndex);

        _playerShipScore.SetCannonScore(cannonIndex);

        if (_shipSpawner != null)
        {
            _shipSpawner.ChangeCannon(cannonIndex);
            _buttonInactiver.InactiveSelectButton(ItemCategory.Cannon, cannonIndex);
        }

        if (cannonIndex >= 0 && cannonIndex < _cannonDatas.Length)
        {
            if (_cannonStatsUI != null)
            {
                _cannonStatsUI.UpdateStats(_cannonDatas[cannonIndex]);
            }
            else
            {
                Debug.LogError("CannonStatsUI referansý ShopPanel'e atanmamýþ!");
            }
        }
        else
        {
            Debug.LogWarning("Seçilen index için CannonData bulunamadý!");
        }
    }

    public void SelectSail(int sailIndex)
    {
        Debug.Log("Seçilen sail Index: " + sailIndex);

        if (_shipSpawner != null)
        {
            _shipSpawner.ChangeSail(sailIndex);
            _buttonInactiver.InactiveSelectButton(ItemCategory.Sail, sailIndex);
        }

        RefreshShipStatsUI();
    }

    public void OpenShipButtons()
    {
        ClearLeftButtons();
        _shipButtons.SetActive(true);
    }

    public void OpenCannonButtons()
    {
        ClearLeftButtons();
        _cannonButtons.SetActive(true);
    }

    public void OpenSailButtons()
    {
        ClearLeftButtons();
        _sailButtons.SetActive(true);
    }

    private void RefreshInitialStats()
    {
        if (_shipSpawner == null) return;

        RefreshShipStatsUI();

        int currentCannonID = _shipSpawner.currentCannonID;
        if (currentCannonID >= 0 && currentCannonID < _cannonDatas.Length)
        {
            if (_cannonStatsUI != null)
            {
                _cannonStatsUI.UpdateStats(_cannonDatas[currentCannonID]);
            }
        }

        if (_buttonInactiver != null)
        {
            _buttonInactiver.InactiveSelectButton(ItemCategory.Ship, _shipSpawner.currentShipID);
            _buttonInactiver.InactiveSelectButton(ItemCategory.Cannon, _shipSpawner.currentCannonID);
            _buttonInactiver.InactiveSelectButton(ItemCategory.Sail, _shipSpawner.currentSailID);
        }

        if (_playerShipScore != null)
        {
            _playerShipScore.SetShipScore(_shipSpawner.currentShipID);
            _playerShipScore.SetCannonScore(_shipSpawner.currentCannonID);
        }
        else
        {
            Debug.LogError("PlayerShipScore referansý ShopPanel'e atanmamýþ!");
        }

        Debug.Log("Dükkan Açýldý: Barlar, Buton Durumlarý ve Toplam Skor mevcut gemi ve topa göre güncellendi!");
    }
    private void RefreshShipStatsUI()
    {
        int shipID = _shipSpawner.currentShipID;
        int sailID = _shipSpawner.currentSailID;

        if (shipID >= 0 && shipID < _shipDatas.Length)
        {
            float speedBonus = 0f;

            if (sailID >= 0 && sailID < _sailDatas.Length)
            {
                speedBonus = _sailDatas[sailID].speedBonus;
            }

            _shipStatsUI.UpdateStats(_shipDatas[shipID], speedBonus);
        }
    }

    private void ClearLeftButtons()
    {
        _shipButtons.SetActive(false);
        _cannonButtons.SetActive(false);
        _sailButtons.SetActive(false);
    }

    private void OnEnable()
    {
        RefreshInitialStats();
    }
}
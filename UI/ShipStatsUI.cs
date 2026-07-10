using UnityEngine;
using UnityEngine.UI;

public class ShipStatsUI : MonoBehaviour
{
    [Header("Fill Images (Mevcut Barlar)")]
    [SerializeField] private Image _speedFill;
    [SerializeField] private Image _healthFill;
    [SerializeField] private Image _cannonFill;

    [Header("Preview Fill Images (Yeþil Preview Barlar)")]
    [SerializeField] private Image _speedPreviewFill;
    [SerializeField] private Image _healthPreviewFill;
    [SerializeField] private Image _cannonPreviewFill;

    [Header("Max Stats (Normalizasyon Ýçin)")]
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _maxHealth = 1000f;
    [SerializeField] private int _maxCannonCount = 10;

    private ShipData _currentShipData;
    private float _currentExtraSpeed;


    public void UpdateStats(ShipData shipData, float extraSpeed = 0f)
    {
        if (shipData == null) return;

        _currentShipData = shipData;
        _currentExtraSpeed = extraSpeed; 

        float totalSpeed = shipData.speed + _currentExtraSpeed;

        _speedFill.fillAmount = Mathf.Clamp01(totalSpeed / _maxSpeed);
        _healthFill.fillAmount = Mathf.Clamp01(shipData.health / _maxHealth);
        _cannonFill.fillAmount = Mathf.Clamp01((float)shipData.cannonCount / _maxCannonCount);

        ClearPreview();
    }

    public void UpdatePreviewStats(ShipData previewShipData, SailData previewSailData)
    {
        ShipData targetShip = previewShipData != null ? previewShipData : _currentShipData;

        if (targetShip == null) return;

        float targetExtraSpeed = previewSailData != null ? previewSailData.speedBonus : _currentExtraSpeed;

        float totalPreviewSpeed = targetShip.speed + targetExtraSpeed;

        _speedPreviewFill.fillAmount = Mathf.Clamp01(totalPreviewSpeed / _maxSpeed);
        _healthPreviewFill.fillAmount = Mathf.Clamp01(targetShip.health / _maxHealth);
        _cannonPreviewFill.fillAmount = Mathf.Clamp01((float)targetShip.cannonCount / _maxCannonCount);

        _speedPreviewFill.gameObject.SetActive(true);
        _healthPreviewFill.gameObject.SetActive(true);
        _cannonPreviewFill.gameObject.SetActive(true);
    }

    public void ClearPreview()
    {
        if (_speedPreviewFill != null)
        {
            _speedPreviewFill.fillAmount = 0f;
            _speedPreviewFill.gameObject.SetActive(false);
        }
        if (_healthPreviewFill != null)
        {
            _healthPreviewFill.fillAmount = 0f;
            _healthPreviewFill.gameObject.SetActive(false);
        }
        if (_cannonPreviewFill != null)
        {
            _cannonPreviewFill.fillAmount = 0f;
            _cannonPreviewFill.gameObject.SetActive(false);
        }
    }
}
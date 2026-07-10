using UnityEngine;
using UnityEngine.UI;

public class CannonStatsUI : MonoBehaviour
{
    [Header("Fill Images (Mevcut Barlar)")]
    [SerializeField] private Image _damageFill;
    [SerializeField] private Image _distanceFill;

    [Header("Preview Fill Images (Yeþil Preview Barlar)")]
    [SerializeField] private Image _damagePreviewFill;
    [SerializeField] private Image _distancePreviewFill;

    [Header("Max Stats (Normalizasyon Ýçin)")]
    [SerializeField] private float _maxDamage = 500f; 
    [SerializeField] private float _maxDistance = 1000f; 

    private CannonData _currentCannonData;

    public void UpdateStats(CannonData cannonData)
    {
        if (cannonData == null) return;
        _currentCannonData = cannonData;

        _damageFill.fillAmount = Mathf.Clamp01(cannonData.damage / _maxDamage);
        _distanceFill.fillAmount = Mathf.Clamp01(cannonData.distance / _maxDistance);

        ClearPreview();
    }


    public void UpdatePreviewStats(CannonData previewCannonData)
    {
        if (previewCannonData == null) return;

        _damagePreviewFill.fillAmount = Mathf.Clamp01(previewCannonData.damage / _maxDamage);
        _distancePreviewFill.fillAmount = Mathf.Clamp01(previewCannonData.distance / _maxDistance);

        _damagePreviewFill.gameObject.SetActive(true);
        _distancePreviewFill.gameObject.SetActive(true);
    }


    public void ClearPreview()
    {
        if (_damagePreviewFill != null)
        {
            _damagePreviewFill.fillAmount = 0f;
            _damagePreviewFill.gameObject.SetActive(false);
        }
        if (_distancePreviewFill != null)
        {
            _distancePreviewFill.fillAmount = 0f;
            _distancePreviewFill.gameObject.SetActive(false);
        }
    }
}
using UnityEngine;
using DavidJalbert;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Sahnede aim bilgisini alacaðýmýz tüm top kontrolcüleri (Sað, Sol vs.).")]
     public CanonAimController[] _aimControllers;

    [Tooltip("Mesafesini deðiþtireceðimiz kamera sistemi.")]
    [SerializeField] private TinyCarCamera _tinyCarCamera;

    [Header("Zoom Settings")]
    [Tooltip("Tam güçte aim alýrken mevcut kameraya eklenecek maksimum uzaklaþma birimi.")]
    [SerializeField] private float _maxExtraZoomDistance = 5f;

    private float _baseDistance;

    private int _flyingBallCount = 0;

    private void Start()
    {
        if (_tinyCarCamera != null)
        {
            _baseDistance = _tinyCarCamera.topDownDistance;
        }
        else
        {
            Debug.LogWarning("CameraController: TinyCarCamera referansý atanmamýþ!");
        }
    }

    public void BallLaunched()
    {
        _flyingBallCount++;
    }

    public void BallHit()
    {
        _flyingBallCount--;
        if (_flyingBallCount < 0) _flyingBallCount = 0; 
    }

    private void Update()
    {
        if (_aimControllers == null || _aimControllers.Length == 0 || _tinyCarCamera == null) return;

        float maxAimRatio = 0f;

        for (int i = 0; i < _aimControllers.Length; i++)
        {
            if (_aimControllers[i] != null && _aimControllers[i].AimProgress > maxAimRatio)
            {
                maxAimRatio = _aimControllers[i].AimProgress;
            }
        }

        float calculatedTargetDistance = _baseDistance + (_maxExtraZoomDistance * maxAimRatio);

        if (_flyingBallCount > 0)
        {

            if (calculatedTargetDistance > _tinyCarCamera.topDownDistance)
            {
                _tinyCarCamera.topDownDistance = calculatedTargetDistance;
            }
        }
        else
        {
            _tinyCarCamera.topDownDistance = calculatedTargetDistance;
        }
    }

    public void SetShipDepency(TinyCarCamera cameraSc)
    {
        _tinyCarCamera = cameraSc;
    }
}
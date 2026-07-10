using UnityEngine;

public class CanonAimController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _canonAim;
    [SerializeField] private Renderer _childRenderer;
    [Tooltip("Mermilerin çýkacaðý noktalar. 2 top varsa 2, 4 top varsa 4 transform ekleyin.")]
    [SerializeField] private Transform[] _firePoints; 
    [SerializeField] private PlayerBulletPool _bulletPool;
    [SerializeField] private PlayerSoundEffect _playerSoundEffect;

    [Header("Aim Settings")]
    [SerializeField] private float _aimDuration = 3f;
    [SerializeField] private float _targetAngleY = -65f;
    [SerializeField] private Gradient _colorGradient;

    [Header("Launch Physics")]
    [Tooltip("X: Sað/Sol Gücü | Y: Yukarý Gücü | Z: Ýleri Gücü")]
    [SerializeField] private Vector3 _maxLaunchForce = new Vector3(30f, 5f, 0f);

    private float _aimTimer = 0f;
    private bool _isAiming = false;
    private MaterialPropertyBlock _propertyBlock;
    private int _colorPropertyID;

    public float AimProgress => Mathf.Clamp01(_aimTimer / _aimDuration);

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _colorPropertyID = Shader.PropertyToID("_Color");
    }

    private void Update()
    {
        HandleAimProgress();
        ApplyTransformAndColor();
    }

    private void HandleAimProgress()
    {
        if (_isAiming)
            _aimTimer = Mathf.Clamp(_aimTimer + Time.deltaTime, 0f, _aimDuration);
    }

    private void ApplyTransformAndColor()
    {
        float progress = _aimTimer / _aimDuration;
        float currentAngleY = Mathf.Lerp(0f, _targetAngleY, progress);
        _canonAim.localRotation = Quaternion.Euler(0f, currentAngleY, 0f);

        if (_childRenderer != null)
        {
            _childRenderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(_colorPropertyID, _colorGradient.Evaluate(progress));
            _childRenderer.SetPropertyBlock(_propertyBlock);
        }
    }

    public void StartAiming()
    {
        _isAiming = true;
        _canonAim.gameObject.SetActive(true);
    }

    public void StopAiming()
    {
        _isAiming = false;
        _playerSoundEffect.PlayCannonFireSoundEffect();
        Fire();

        _aimTimer = 0f;
        ApplyTransformAndColor();
        _canonAim.gameObject.SetActive(false);
    }

    private void Fire()
    {
        float powerMultiplier = _aimTimer / _aimDuration;
        Vector3 localForce = _maxLaunchForce * powerMultiplier;

        foreach (Transform firePoint in _firePoints)
        {
            if (firePoint == null) continue;

            CannonBall ball = _bulletPool.GetBallFromPool();

            ball.transform.position = firePoint.position;
            ball.gameObject.SetActive(true);

            Vector3 finalForce = firePoint.TransformDirection(localForce);
            ball.Launch(finalForce);
        }
        
    }
    public void SetCannonDistance(float distance,float distanceY)
    {
        _maxLaunchForce = new Vector3(distance, distanceY, 0f);

    }
    public void SetShipDepency(PlayerBulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }
}
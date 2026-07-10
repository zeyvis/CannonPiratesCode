using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Bağlantılar")]
    [SerializeField] private PlayerSoundEffect _playerSoundEffect;
    private VirtualJoystick _joystick;
    private Rigidbody rb;

    [Header("Görsel Efektler")]
    [Tooltip("Gemi hareket ederken oynatılacak Particle System")]
    [SerializeField] private ParticleSystem _movementParticles;

    [Header("Hareket ve Hız Ayarları")]
    [Tooltip("Gemi dönüş yaparken (açı tam tutturulamamışken) gideceği yavaş hız")]
    [SerializeField] private float turningSpeed = 3f;

    [Tooltip("Doğru açıyı bulduğunda tam gaz çıkacağı maksimum hız")]
    [SerializeField] private float maxSpeed = 10f;

    [Tooltip("Joystick bırakıldığında suyun sürtünme etkisiyle ne kadar çabuk duracağı (Düşük değer = daha çok kayma)")]
    [SerializeField] private float deceleration = 2f;

    [Tooltip("Maksimum hıza ne kadar sürede ivmeleneceği")]
    [SerializeField] private float acceleration = 4f;

    [Header("Dönüş Ayarları")]
    [Tooltip("Geminin saniyede kendi etrafında dönme hızı (Derece cinsinden)")]
    [SerializeField] private float rotationSpeed = 120f;

    [Tooltip("Hedef açıya ne kadar yaklaşıldığında tam hıza geçilecek? (+/- Derece)")]
    [SerializeField] private float angleTolerance = 8f;

    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0f;
        rb.angularDrag = 0f;
    }

    private void FixedUpdate()
    {
        HandleMovementAndRotation();
        HandleParticleSystem();
    }

    private bool TryGetInput(out Vector2 inputVector)
    {
        if (_joystick != null && _joystick.HasInput)
        {
            inputVector = _joystick.InputVector;
            return true;
        }

#if UNITY_EDITOR
        float x = 0f, y = 0f;
        if (Input.GetKey(KeyCode.A)) x -= 1f;
        if (Input.GetKey(KeyCode.D)) x += 1f;
        if (Input.GetKey(KeyCode.W)) y += 1f;
        if (Input.GetKey(KeyCode.S)) y -= 1f;

        if (x != 0f || y != 0f)
        {
            inputVector = new Vector2(x, y).normalized;
            return true;
        }
#endif

        inputVector = Vector2.zero;
        return false;
    }

    private void HandleMovementAndRotation()
    {
        float targetSpeed = 0f;

        if (TryGetInput(out Vector2 input))
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;

            float currentAngle = transform.eulerAngles.y;
            float deltaAngle = Mathf.DeltaAngle(currentAngle, targetAngle);

            bool isAligned = Mathf.Abs(deltaAngle) <= angleTolerance;
            targetSpeed = isAligned ? maxSpeed : turningSpeed;

            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        float lerpSpeed = (targetSpeed > currentSpeed) ? acceleration : deceleration;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, lerpSpeed * Time.fixedDeltaTime);

        rb.velocity = transform.forward * currentSpeed;
        float speedRatio = maxSpeed > 0f ? (currentSpeed / maxSpeed) : 0f;

        if (_playerSoundEffect != null)
        {
            _playerSoundEffect.UpdateMovementSound(speedRatio);
        }
    }

    private void HandleParticleSystem()
    {
        if (_movementParticles == null) return;

        if (TryGetInput(out _))
        {
            if (!_movementParticles.isPlaying)
            {
                _movementParticles.Play();
            }
        }
        else
        {
            if (_movementParticles.isPlaying)
            {
                _movementParticles.Stop();
            }
        }
    }

    public void SetPlayerShipSpeed(float maxShipSpeed, float sailSpeedBonus)
    {
        maxSpeed = maxShipSpeed + sailSpeedBonus;
        turningSpeed = maxSpeed - 2;
    }

    public void SetShipDepency(VirtualJoystick joystick)
    {
        _joystick = joystick;
    }
}
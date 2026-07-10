using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Görsel Efektler")]
    [Tooltip("Düşman gemisi hareket ederken oynatılacak Particle System")]
    [SerializeField] private ParticleSystem _movementParticles;

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        HandleParticleSystem();
    }


    public void MoveStraight(Transform playerPos, float rotationSpeed)
    {
        Vector3 dirToPlayer = (playerPos.position - transform.position).normalized;

        if (dirToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            playerPos.position,
            _moveSpeed * Time.deltaTime
        );
    }

    public bool MoveToBroadside(Transform playerPos, int direction, float rotationSpeed)
    {
        Vector3 dirToPlayer = (playerPos.position - transform.position).normalized;

        Vector3 targetForward = Quaternion.Euler(0, 90f * direction, 0) * dirToPlayer;
        Quaternion targetRotation = Quaternion.LookRotation(targetForward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
       
        float alignment = Vector3.Dot(transform.forward, dirToPlayer);

       
        if (Mathf.Abs(alignment) < 0.05f)
        {
            return true;
        }

        return false;
    }

    
    private void HandleParticleSystem()
    {
        if (_movementParticles == null) return;

        
        float distanceMoved = Vector3.Distance(transform.position, _lastPosition);
        float currentSpeed = distanceMoved / Time.deltaTime;

        
        _lastPosition = transform.position;

       
        if (currentSpeed > 0.1f)
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
   
}
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    private enum AIState { Approaching, Flanking, ReadyToFire }
    private AIState _currentState = AIState.Approaching;

    [Header("Distance Settings (SqrMagnitude)")]
    [SerializeField] private float _stopDistance = 225f;
    [SerializeField] private float _fireToleranceDistance = 250f;

    [Header("Movement & Attack")]
    [SerializeField] private float _rotationSpeed = 60f;
    [SerializeField] private EnemyShipMovement _enemyShipMovement;
    [SerializeField] private EnemyShipAttack _enemyShipAttack;

    private Transform _playerPos;
    private int _flankDirection = 1;

    private float _fireCooldown = 3.5f;
    private float _nextFireTime = 0f;

    private bool _hasFiredThisRound = false;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _playerPos = playerObj.transform;
    }

    private void Update()
    {
        if (_playerPos == null) return;

        float sqrDistanceToPlayer = (transform.position - _playerPos.position).sqrMagnitude;

        switch (_currentState)
        {
            case AIState.Approaching:
                if (sqrDistanceToPlayer > _stopDistance)
                {
                    _enemyShipMovement.MoveStraight(_playerPos, _rotationSpeed);
                }
                else
                {
                    DecideRightOrLeft();
                    _hasFiredThisRound = false;
                    _currentState = AIState.Flanking;
                }
                break;

            case AIState.Flanking:
                bool isAligned = _enemyShipMovement.MoveToBroadside(_playerPos, _flankDirection, _rotationSpeed);
                if (isAligned)
                {
                    _currentState = AIState.ReadyToFire;
                }
                break;

            case AIState.ReadyToFire:
                if (_hasFiredThisRound && sqrDistanceToPlayer > _fireToleranceDistance)
                {
                    _currentState = AIState.Approaching;
                    break;
                }

                if (Time.time >= _nextFireTime)
                {
                    _enemyShipAttack.Fire(_flankDirection, _playerPos);
                    _hasFiredThisRound = true;
                    _nextFireTime = Time.time + _fireCooldown;
                }
                break;
        }
    }

    private void DecideRightOrLeft()
    {
        _flankDirection = Random.Range(0, 2) == 0 ? -1 : 1;
    }
}
using System;
using System.Collections;
using UnityEngine;

public class EnemyShipHealth : MonoBehaviour
{
    [SerializeField] private BoxCollider _mainCollider;
    [Tooltip("Ne kadar sürede batsýn(arttýrýrsan hýzlý batar)")]
    [SerializeField] private float _sinkTimer = 2f;
    [Tooltip("Nereye kadar batsýn")]
    [SerializeField] private float _targerSinkPosY = -0.7f;

    [SerializeField] private EnemyShipAI _enemyShipAI;

    public static event Action OnEnemyDeath;


    private bool _isDead = false;

    public void Die()
    {
        if (_isDead) return;

        _isDead = true;

        if (_mainCollider != null)
        {
            _mainCollider.enabled = false;
        }

        if (_enemyShipAI != null)
        {
            _enemyShipAI.enabled = false;
        }

        StartCoroutine(SinkShipRoutine());
    }

    private IEnumerator SinkShipRoutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, _targerSinkPosY, startPosition.z);

        float duration = _sinkTimer;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        OnEnemyDeath?.Invoke();

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        ContinueButton.OnContinueGame += Die;
    }

    private void OnDisable()
    {
        ContinueButton.OnContinueGame -= Die;
    }
}
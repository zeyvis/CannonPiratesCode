using UnityEngine;
using System.Collections; 

public class EnemyShipAttack : MonoBehaviour
{
    [Header("Ammo Settings")]
    [Tooltip("Sol taraftaki tüm top çýkýþ noktalarýný buraya ekleyin.")]
    [SerializeField] private Transform[] _leftCannons;

    [Tooltip("Sað taraftaki tüm top çýkýþ noktalarýný buraya ekleyin.")]
    [SerializeField] private Transform[] _rightCannons;

    [Header("Attack Mechanics")]
    [SerializeField] private float _bulletSpeed = 15f; 
    [SerializeField] private float _missMargin = 5f;   
    [SerializeField] private float _damageAmount;

    [Tooltip("Toplarýn peþ peþe ateþlenme süresi. Hepsini ayný anda atmak için 0 yapýn.")]
    [SerializeField] private float _fireDelay = 0.15f; 

    [SerializeField] private EnemySoundController _enemySoundController;


    public void Fire(int flankDirection, Transform playerPos)
    {
        _enemySoundController.PlayCannonSoundEffect();
        Transform[] activeCannons = (flankDirection == 1) ? _rightCannons : _leftCannons;

        StartCoroutine(FireBroadsideRoutine(activeCannons, playerPos));
    }

    private IEnumerator FireBroadsideRoutine(Transform[] cannons, Transform playerPos)
    {
        foreach (Transform firePoint in cannons)
        {
            if (firePoint == null) continue;

            EnemyBullet bulletScript = EnemyBulletPool.Instance.GetBullet(firePoint.position, firePoint.rotation,_damageAmount);

            Vector3 firePosXZ = new Vector3(firePoint.position.x, 0, firePoint.position.z);
            Vector3 playerPosXZ = new Vector3(playerPos.position.x, 0, playerPos.position.z);

            Vector3 directionToPlayer = (playerPosXZ - firePosXZ).normalized;
            float actualDistance = Vector3.Distance(firePosXZ, playerPosXZ);

            float randomizedDistance = actualDistance + Random.Range(-_missMargin, _missMargin);

            Vector3 targetLandingPoint = firePosXZ + (directionToPlayer * randomizedDistance);
            targetLandingPoint.y = 0f;

            Rigidbody rb = bulletScript.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float timeToHit = randomizedDistance / _bulletSpeed;

                Vector3 velocityXZ = (targetLandingPoint - firePosXZ) / timeToHit;

                float velocityY = (targetLandingPoint.y - firePoint.position.y + 0.5f * Mathf.Abs(Physics.gravity.y) * (timeToHit * timeToHit)) / timeToHit;

                rb.velocity = new Vector3(velocityXZ.x, velocityY, velocityXZ.z);
            }

            if (_fireDelay > 0f)
            {
                yield return new WaitForSeconds(_fireDelay);
            }
        }
    }

    public void SetEnemyShipDamageAmount(float damageAmount)
    {
        _damageAmount = damageAmount;
    }
}
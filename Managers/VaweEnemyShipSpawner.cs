using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaweEnemyShipSpawner : MonoBehaviour
{
    [SerializeField] private PlayerShipScore _playerShipScore;
    [SerializeField] private float _spawnTime = 17f;
    [SerializeField] private int _maxAliveEnemy = 2;

    [Header("Database")]
    [SerializeField] private GameObject[] _enemyShip;
    [SerializeField] private GameObject[] _enemyCannons;
    [SerializeField] private GameObject[] _enemySails;
    [SerializeField] private int[] _cannonDamages;
    [SerializeField] private Transform[] _SpawnPos;

    private EnemyShipAttack _enemyShipAttack;
    private GameObject _spawnedShip;
    private int spawnPosIndex;
    private float spawnTimer;
    private int _aliveEnemyAmount = 0;

    private bool _isReadyToSpawn = false;

  
    public void SetDifficultyParameters(float newSpawnTime, int newMaxAliveEnemy)
    {
        _spawnTime = newSpawnTime;
        _maxAliveEnemy = newMaxAliveEnemy;
    }

    private void Update()
    {

        if (!_isReadyToSpawn)
            return;


        spawnTimer += Time.deltaTime;

        if ((spawnTimer >= _spawnTime || _aliveEnemyAmount == 0) && _aliveEnemyAmount <= _maxAliveEnemy)
        {
            SpawnEnemyShip(ChooseWhichShip(PlayerPowerLevel()));
            spawnTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnEnemyShip(ChooseWhichShip(PlayerPowerLevel()));
        }
    }

    private void SpawnEnemyShip(GameObject enemyShip)
    {
        _spawnedShip = Instantiate(enemyShip, _SpawnPos[SelectSpawnPos()].position, Quaternion.identity);
        SelectCannonAndSetDamage(PlayerPowerLevel(), _spawnedShip.GetComponent<ShipEquipper>());
        _aliveEnemyAmount++;
    }

    private void SelectCannonAndSetDamage(int level, ShipEquipper shipEquipper)
    {
        int randomCannonlvl1 = Random.Range(0, 2);
        int randomCannonlvl2 = Random.Range(1, 3);
        int randomCannonlvl3 = Random.Range(2, 4);
        int randomSail = Random.Range(0, 3);

        if (level == 0)
        {
            shipEquipper.EquipShip(_enemyCannons[randomCannonlvl1], _enemySails[randomSail]);
            SetDamageAmount(_spawnedShip, randomCannonlvl1);
        }
        else if (level == 1)
        {
            shipEquipper.EquipShip(_enemyCannons[randomCannonlvl2], _enemySails[randomSail]);
            SetDamageAmount(_spawnedShip, randomCannonlvl2);
        }
        else
        {
            shipEquipper.EquipShip(_enemyCannons[randomCannonlvl3], _enemySails[randomSail]);
            SetDamageAmount(_spawnedShip, randomCannonlvl3);
        }
    }

    private GameObject ChooseWhichShip(int level)
    {
        int randonChance = Random.Range(0, 2);
        if (level == 0)
        {
            if (randonChance == 0)
            {
                return _enemyShip[0];
            }
            else
            {
                return _enemyShip[1];
            }
        }
        else if (level == 1)
        {
            if (randonChance == 0)
            {
                return _enemyShip[2];
            }
            else
            {
                return _enemyShip[3];
            }
        }
        else
        {
            return _enemyShip[4];
        }
    }

    private void SetDamageAmount(GameObject currentSpawnedShip, int cannonIndex)
    {
        _enemyShipAttack = currentSpawnedShip.GetComponent<EnemyShipAttack>();
        int calculatedDamage = 10; // Varsayýlan hasar

        if (cannonIndex < _cannonDamages.Length)
        {
            calculatedDamage = _cannonDamages[cannonIndex];
        }
        else
        {
            Debug.LogWarning("Uyarý: _cannonDamages dizisinde yeterli eleman yok! Varsayýlan hasar (10) uygulandý.");
        }
        _enemyShipAttack.SetEnemyShipDamageAmount(calculatedDamage);
    }

    private int SelectSpawnPos()
    {
        spawnPosIndex++;
        if (spawnPosIndex >= _SpawnPos.Length)
        {
            spawnPosIndex = 0;
        }
        return spawnPosIndex;
    }

    private int PlayerPowerLevel()
    {
        float playerShipScore = _playerShipScore.GetScore();
        if (playerShipScore < 6.1)
        {
            return 0;
        }
        else if (playerShipScore < 9.5)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    private void DecreaseAliveEnemyAmount()
    {
        _aliveEnemyAmount--;
    }
    private void StartSpawn()
    {
        _isReadyToSpawn = true;
    }

    public void OnEnable()
    {
        EnemyShipHealth.OnEnemyDeath += DecreaseAliveEnemyAmount;
        EntryPanel.onStartGame += StartSpawn;
    }

    public void OnDisable()
    {
        EnemyShipHealth.OnEnemyDeath -= DecreaseAliveEnemyAmount;
        EntryPanel.onStartGame -= StartSpawn;
    }
}
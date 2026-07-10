using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShipSpawnConfig
{
    [Header("Ship Settings")]
    public GameObject shipPrefab;   
    public GameObject cannonPrefab; 

    public GameObject sailPrefab;   

    [Header("Spawn Settings")]
    public int spawnPointIndex;      
}

[CreateAssetMenu(fileName = "New Level Data", menuName = "Game Data/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Information")]
    public int levelNumber;

    [Header("Enemies in this Level")]
    public List<ShipSpawnConfig> enemiesToSpawn;
}
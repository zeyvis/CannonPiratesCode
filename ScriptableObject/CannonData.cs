using UnityEngine;

[CreateAssetMenu(fileName = "NewCannonData", menuName = "PirateGame/Cannon Data")]
public class CannonData : ScriptableObject
{
    [Header("Cannon Info")]
    public int cannonID;
    public string cannonName;

    [Header("Cannon Stats (Ýstatistikler)")]
    public float damage;      
    public float distance;    
    public float distanceY;

    [Header("Cannon Prefab")]
    public GameObject cannonPrefab; 
}
using UnityEngine;

[CreateAssetMenu(fileName = "NewShipData", menuName = "PirateGame/Ship Data")]
public class ShipData : ScriptableObject
{
    [Header("Ship Info")]
    public int shipID; 
    public string shipName;

    [Header("Ship Prefab")]
    public GameObject shipPrefab; 

    [Header("Ship Stats (Ýstatistikler)")]
    public float speed;
    public float health;
    public int cannonCount;
}
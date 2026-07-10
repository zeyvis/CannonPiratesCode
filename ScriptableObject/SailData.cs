using UnityEngine;

[CreateAssetMenu(fileName = "NewSailData", menuName = "PirateGame/Sail Data")]
public class SailData : ScriptableObject
{
    [Header("Sail Info")]
    public int sailID;
    public string sailName;

    [Tooltip("Gemi hýzýna olan etkisi (Çarpan veya ekstra deðer olabilir)")]
    public float speedBonus;

    [Header("Sail Prefab")]
    public GameObject sailPrefab;
}
using UnityEngine;
using System.Collections.Generic;

public class ShipSpawner : MonoBehaviour
{
    [Header("Databases")]
    public ShipDatabase shipDatabase;
    public List<CannonData> availableCannons; 
    public List<SailData> availableSails;    

    [Header("Spawn Settings")]
    public Transform spawnPoint;



    [Header("Testing / Current Selection")]
    public int currentShipID = 0;
    public int currentCannonID = 0;
    public int currentSailID = 0;

    private GameObject currentShipInstance;

    [SerializeField] private PlayerEquipper _playerEquipper;
    [SerializeField] private PlayerShipScore _playerShipScore;

    private const string PREF_SHIP_ID = "SelectedShipID";
    private const string PREF_CANNON_ID = "SelectedCannonID";
    private const string PREF_SAIL_ID = "SelectedSailID";

    private void Start()
    {
        currentShipID = PlayerPrefs.GetInt(PREF_SHIP_ID, 0);
        currentCannonID = PlayerPrefs.GetInt(PREF_CANNON_ID, 0);
        currentSailID = PlayerPrefs.GetInt(PREF_SAIL_ID, 0);

        SpawnShip(currentShipID, currentCannonID, currentSailID);
    }

    public void ChangeShip(int newShipID)
    {
        currentShipID = newShipID;

        PlayerPrefs.SetInt(PREF_SHIP_ID, currentShipID);
        PlayerPrefs.Save();

        SpawnShip(currentShipID, currentCannonID, currentSailID);
    }

    public void ChangeCannon(int newCannonID)
    {
        currentCannonID = newCannonID;

        PlayerPrefs.SetInt(PREF_CANNON_ID, currentCannonID);
        PlayerPrefs.Save();

        SpawnShip(currentShipID, currentCannonID, currentSailID);
    }

    public void ChangeSail(int newSailID)
    {
        currentSailID = newSailID;

        PlayerPrefs.SetInt(PREF_SAIL_ID, currentSailID);
        PlayerPrefs.Save();

        SpawnShip(currentShipID, currentCannonID, currentSailID);
    }

    public void SpawnShip(int shipID, int cannonID, int sailID)
    {
        ShipData selectedShipData = shipDatabase.GetShipByID(shipID);
        CannonData selectedCannonData = GetCannonByID(cannonID);
        SailData selectedSailData = GetSailByID(sailID);

        if (currentShipInstance != null)
        {
            Destroy(currentShipInstance);
        }

        currentShipInstance = Instantiate(selectedShipData.shipPrefab, spawnPoint.position, spawnPoint.rotation);

        _playerEquipper.SetPlayerShip(
            currentShipInstance,
            selectedShipData.speed,
            selectedShipData.health,
            selectedCannonData.distance,
            selectedCannonData.distanceY,
            selectedCannonData.damage,
            selectedSailData.speedBonus
        );

        EquipCannons(currentShipInstance, selectedCannonData);
        EquipSails(currentShipInstance, selectedSailData);

        if (_playerShipScore != null)
        {
            _playerShipScore.SetShipScore(shipID);
            _playerShipScore.SetCannonScore(cannonID);
        }

       
    }

    private void EquipCannons(GameObject ship, CannonData cannonData)
    {
        ShipSockets sockets = ship.GetComponent<ShipSockets>();

        if (sockets == null || sockets.cannonSockets.Count == 0)
        {
            Debug.LogWarning("Bu gemide ShipSockets scripti yok veya top soketi atanmamýþ!");
            return;
        }

        Vector3 actualShipScale = ship.transform.lossyScale;
        Vector3 refScale = sockets.referenceShipScale;

        foreach (Transform socket in sockets.cannonSockets)
        {
            GameObject newCannon = Instantiate(cannonData.cannonPrefab, socket);

            newCannon.transform.localPosition = Vector3.zero;
            newCannon.transform.localRotation = Quaternion.identity;

            Vector3 prefabScale = cannonData.cannonPrefab.transform.localScale;

            newCannon.transform.localScale = new Vector3(
                (refScale.x / actualShipScale.x) * prefabScale.x,
                (refScale.y / actualShipScale.y) * prefabScale.y,
                (refScale.z / actualShipScale.z) * prefabScale.z
            );
        }
    }

    private void EquipSails(GameObject ship, SailData sailData)
    {
        ShipSockets sockets = ship.GetComponent<ShipSockets>();

        if (sockets == null || sockets.sailSockets.Count == 0)
        {
            Debug.LogWarning("Bu gemide yelken soketi yok!");
            return;
        }

        Vector3 actualShipScale = ship.transform.lossyScale;
        Vector3 refScale = sockets.referenceShipScale;

        foreach (Transform socket in sockets.sailSockets)
        {
            GameObject newSail = Instantiate(sailData.sailPrefab, socket);

            newSail.transform.localPosition = Vector3.zero;
            newSail.transform.localRotation = Quaternion.identity;

            Vector3 prefabScale = sailData.sailPrefab.transform.localScale;

            newSail.transform.localScale = new Vector3(
                (refScale.x / actualShipScale.x) * prefabScale.x,
                (refScale.y / actualShipScale.y) * prefabScale.y,
                (refScale.z / actualShipScale.z) * prefabScale.z
            );
        }
    }

    public void CalculatePlayerScore()
    {

    }

    private CannonData GetCannonByID(int id)
    {
        foreach (var cannon in availableCannons)
        {
            if (cannon.cannonID == id)
                return cannon;
        }
        return availableCannons[0];
    }

    private SailData GetSailByID(int id)
    {
        foreach (var sail in availableSails)
        {
            if (sail.sailID == id)
                return sail;
        }
        return availableSails[0];
    }


    [ContextMenu("Reset Saved Selections (PlayerPrefs)")]
    public void ResetSavedSelections()
    {
        currentShipID = 0;
        currentCannonID = 0;
        currentSailID = 0;

        PlayerPrefs.SetInt(PREF_SHIP_ID, 0);
        PlayerPrefs.SetInt(PREF_CANNON_ID, 0);
        PlayerPrefs.SetInt(PREF_SAIL_ID, 0);
        PlayerPrefs.Save();

        Debug.Log("Kayýtlý gemi, top ve yelken seçimleri baþarýyla sýfýrlandý!");

        if (Application.isPlaying)
        {
            SpawnShip(currentShipID, currentCannonID, currentSailID);
        }
    }
}
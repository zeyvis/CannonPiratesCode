using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShipDatabase", menuName = "PirateGame/Ship Database")]
public class ShipDatabase : ScriptableObject
{
    public List<ShipData> allShips;

    public ShipData GetShipByID(int id)
    {
        foreach (var ship in allShips)
        {
            if (ship.shipID == id)
                return ship;
        }

        Debug.LogWarning("Gemi ID'si bulunamadý: " + id + ". Varsayýlan olarak ilk gemi döndürülüyor.");
        return allShips[0];
    }
}
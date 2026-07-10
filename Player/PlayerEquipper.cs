using DavidJalbert;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipper : MonoBehaviour
{
    [SerializeField] private VirtualJoystick _joystick;
    [SerializeField] private TinyCarCamera _cameraSc;
    [SerializeField] private AimButton _rAimButton;
    [SerializeField] private AimButton _lAimButton;
    [SerializeField] private PlayerBulletPool _pBulletPool;
    


    private ShipController _shipController;
    private CameraController _cameraController;
    private CanonAimController _rCannonAimController;
    private CanonAimController _lCannonAimController;
    private ShipHealth _playerShipHealth;


    public void SetPlayerShip(
        GameObject player, float currentShipSpeed,float currentShipHealth,
        float currentCannonDistance,float currentCannonDisranceY,float currentCannonDamage,
        float currentSailSpeedBonus
        )
    {
        _shipController=player.GetComponent<ShipController>();
        _playerShipHealth=player.GetComponent<ShipHealth>();

        _shipController.SetShipDepency(_joystick);
        //Adjust the speed and health of the current player ship data
        _shipController.SetPlayerShipSpeed(currentShipSpeed, currentSailSpeedBonus);
        _playerShipHealth.SetShipPartsHealth(currentShipHealth);
        


        _cameraController= player.GetComponent<CameraController>();
        _cameraController.SetShipDepency(_cameraSc);

        _lCannonAimController=_cameraController._aimControllers[0];
        _lCannonAimController.SetShipDepency(_pBulletPool);
        _rCannonAimController = _cameraController._aimControllers[1];
        _rCannonAimController.SetShipDepency(_pBulletPool);

        //to adjust the cannon range and power of the existing vessel
        _rCannonAimController.SetCannonDistance(currentCannonDistance,currentCannonDisranceY);
        _lCannonAimController.SetCannonDistance(-currentCannonDistance, currentCannonDisranceY);
        _pBulletPool.SetBulletDamage(currentCannonDamage);


        _lAimButton.SetShipDepency(_lCannonAimController);
        _rAimButton.SetShipDepency(_rCannonAimController);



        _cameraSc.whatToFollow = player.transform;
    }
}

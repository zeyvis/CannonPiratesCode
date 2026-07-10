using UnityEngine;
using UnityEngine.EventSystems;

public class AimButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CanonAimController _aimController;

    [Header("Editor Test Settings")]
    [SerializeField] private KeyCode testKey = KeyCode.None;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_aimController != null)
        {
            _aimController.StartAiming();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_aimController != null)
        {
            _aimController.StopAiming();
        }
    }

    private void Update()
    {
        if (testKey != KeyCode.None && _aimController != null)
        {
            if (Input.GetKeyDown(testKey))
            {
                _aimController.StartAiming();
            }

            if (Input.GetKeyUp(testKey))
            {
                _aimController.StopAiming();
            }
        }
    }

    public void SetShipDepency(CanonAimController aimController)
    {
        _aimController = aimController;
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI Referanslarý")]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handler;

    [Header("Ayarlar")]
    [SerializeField] private float joystickRadius = 100f; 

    private Vector2 inputVector;

    public Vector2 InputVector => inputVector;
    public bool HasInput => inputVector.sqrMagnitude > 0.01f;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / background.sizeDelta.x) * 2f;
            position.y = (position.y / background.sizeDelta.y) * 2f;

            inputVector = new Vector2(position.x, position.y);

            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            handler.anchoredPosition = new Vector2(
                inputVector.x * joystickRadius,
                inputVector.y * joystickRadius
            );
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handler.anchoredPosition = Vector2.zero;
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum Parts
{
    frontPart=0,
    midPart=1,
    backPart=2
}
public class ShipStatus : MonoBehaviour
{
    public static ShipStatus Instance { get; private set; }
    [SerializeField] private Image _frontPartImage;
    [SerializeField] private Image _midPartImage;
    [SerializeField] private Image _backPartImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;            
        }
    }
    public void SetShipImage(int x, Color color)
    {
        Parts selectedParts = (Parts)x;

        if (selectedParts == Parts.frontPart)
        {
            _frontPartImage.color = color;
        }
        else if (selectedParts == Parts.midPart)
        {
            _midPartImage.color = color;
        }
        else
        {
            _backPartImage.color = color;
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBobAnimate : MonoBehaviour
{
    [Header("Dalga Ayarlarý")]
    [Tooltip("Geminin inip çýkma hýzý")]
    public float bobbingSpeed = 1.5f;
    [Tooltip("Geminin ne kadar yukarý/aþaðý hareket edeceði")]
    public float bobbingAmount = 0.5f;

    [Header("Yalpalanma Ayarlarý (Rotation)")]
    [Tooltip("Saða ve sola yatma miktarý (Roll)")]
    public float rollAmount = 5f;
    [Tooltip("Öne ve arkaya yatma miktarý (Pitch)")]
    public float pitchAmount = 3f;
    [Tooltip("Yalpalanma hýzý çarpaný")]
    public float rotationSpeedMultiplier = 0.8f;

    private float inspectorBobAmount;
    private float inspectorRollAmount;
    private float inspectorPitchAmount;

   
    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
       
        inspectorBobAmount = bobbingAmount;
        inspectorRollAmount = rollAmount;
        inspectorPitchAmount = pitchAmount;


        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }

    void Update()
    {
        float time = Time.time * bobbingSpeed;


        float newY = startPos.y + (Mathf.Sin(time) * bobbingAmount);

        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);


        float pitch = Mathf.Sin(time * rotationSpeedMultiplier) * pitchAmount; 
        float roll = Mathf.Cos(time * rotationSpeedMultiplier * 1.2f) * rollAmount; 

        Quaternion waveRotation = Quaternion.Euler(pitch, 0f, roll);
        transform.localRotation = startRot * waveRotation;
    }

    public void SetBobtoShop()
    {
            bobbingAmount = 0.05f;
            pitchAmount = 4f;
            rollAmount = 4f;     
    }
    private void SetBobtoPlay()
    {
        bobbingAmount = inspectorBobAmount;
        pitchAmount = inspectorPitchAmount;
        rollAmount = inspectorRollAmount;
    }
    private void OnEnable()
    {
        EntryPanel.onShop += SetBobtoShop;
        EntryPanel.onPlay += SetBobtoPlay;
    }
    private void OnDisable() 
    {
        EntryPanel.onShop -= SetBobtoShop;
        EntryPanel.onPlay -= SetBobtoPlay;
    }

}
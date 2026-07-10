using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class ButtonCancelAnimation : MonoBehaviour
{
    [Header("Sallanma Animasyonu Ayarlarý")]
    [SerializeField] private float duration = 0.4f;   
    [SerializeField] private float shakeSpeed = 50f;     
    [SerializeField] private float shakeAmount = 10f;   

    [Header("Renk (Flash) Animasyonu Ayarlarý")]
    [SerializeField] private Color flashColor = Color.red; 
    [SerializeField] private float flashSpeed = 20f;         

    public void PlayCancelAnimation(Button button)
    {
        if (button == null) return;


        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("PlayCancelAnimation: Verilen butonun üzerinde Image component'i bulunamadý!");
            return;
        }

        StartCoroutine(AnimateButtonEffects(button, buttonImage));
    }

    private IEnumerator AnimateButtonEffects(Button button, Image buttonImage)
    {
        float elapsedTime = 0f;

        Quaternion originalRotation = button.transform.localRotation;
        Color originalColor = buttonImage.color; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;


            float zRotation = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            button.transform.localRotation = originalRotation * Quaternion.Euler(0f, 0f, zRotation);


            float sinRaw = Mathf.Sin(Time.time * flashSpeed);
            float lerpValue = (sinRaw + 1f) / 2f;

            buttonImage.color = Color.Lerp(originalColor, flashColor, lerpValue);

            yield return null;
        }

        button.transform.localRotation = originalRotation;
        buttonImage.color = originalColor;
    }
}
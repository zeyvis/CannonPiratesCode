using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    [Header("Animasyon Ayarlarý")]
    public float scaleSpeed = 3f;     
    public float scaleAmount = 0.05f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;

        transform.localScale = startScale + new Vector3(pulse, pulse, pulse);
    }
}
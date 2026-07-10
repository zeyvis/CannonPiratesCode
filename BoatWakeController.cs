using UnityEngine;

public class BoatWakeController : MonoBehaviour
{
    [Header("Ayarlar")]
    public LayerMask waterLayer;
    public float recordInterval = 0.015f;

    private Vector2 lastRecordedUV;

    void FixedUpdate()
    {
        Vector3 origin = transform.position + (Vector3.up * 2f);
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20f, waterLayer))
        {
            Vector2 currentUV = hit.textureCoord;

            if (currentUV == Vector2.zero) return;

            float distance = Vector2.Distance(lastRecordedUV, currentUV);

            if (distance >= recordInterval)
            {
                // Artýk suya kendimiz yazmýyoruz, Müdüre haber veriyoruz!
                if (WaterWakeManager.Instance != null)
                {
                    WaterWakeManager.Instance.AddWakePoint(currentUV);
                }

                lastRecordedUV = currentUV;
            }
        }
    }
}
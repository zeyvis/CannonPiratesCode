using UnityEngine;

public class WaterWakeManager : MonoBehaviour
{
    // Herkesin bu müdüre ulaþabilmesi için Singleton (Tekil) yapý
    public static WaterWakeManager Instance;

    [Header("Su Ayarlarý")]
    public MeshRenderer waterPlaneRenderer;
    private Material waterMaterial;

    // Artýk 30 noktalýk dev bir havuzumuz var
    private Vector4[] globalWakePoints = new Vector4[30];
    private int globalIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (waterPlaneRenderer != null)
        {
            waterMaterial = waterPlaneRenderer.material;
        }
    }

    // Gemiler sadece bu fonksiyonu çaðýrýp UV yollayacak
    public void AddWakePoint(Vector2 uv)
    {
        globalWakePoints[globalIndex] = new Vector4(uv.x, uv.y, Time.timeSinceLevelLoad, 0);
        globalIndex = (globalIndex + 1) % 30; // 30 noktayý sýrayla dön

        if (waterMaterial != null)
        {
            // Tüm gemilerin ortak listesini suya yazdýr
            waterMaterial.SetVectorArray("_InputCentre", globalWakePoints);
        }
    }
}
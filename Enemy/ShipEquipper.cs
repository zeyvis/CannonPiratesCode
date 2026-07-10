using UnityEngine;

public class ShipEquipper : MonoBehaviour
{
    [Header("Mount Points (Baðlantý Noktalarý)")]
    [Tooltip("Geminin sað ve sol bordasýndaki top yuvalarýný (boþ objeleri) buraya sürükleyin.")]
    [SerializeField] private Transform[] cannonSlots;

    [Tooltip("Geminin direklerini (yelken yuvalarýný) buraya sürükleyin.")]
    [SerializeField] private Transform[] sailSlots;

    [Header("Visuals (Görsel Bileþenler)")]
    [Tooltip("Geminin gövde parçalarýný (MeshRenderer) buraya sürükleyin veya boþ býrakýn, kod kendi bulsun.")]
    [SerializeField] private MeshRenderer[] shipMeshRenderers; 

    [Tooltip("Sadece bu gemi tipine özel hazýrlanmýþ texture varyasyonlarý")]
    [SerializeField] private Texture2D[] availableTextures;

    private static MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        if (propertyBlock == null)
        {
            propertyBlock = new MaterialPropertyBlock();
        }

       
        if (shipMeshRenderers == null || shipMeshRenderers.Length == 0)
        {
            shipMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        }
    }

    public void EquipShip(GameObject cannonPrefab, GameObject sailPrefab)
    {
        EquipCannons(cannonPrefab);
        EquipSails(sailPrefab);
        ApplyRandomTexture();
    }

    private void EquipCannons(GameObject cannonPrefab)
    {
        if (cannonPrefab == null || cannonSlots == null || cannonSlots.Length == 0)
            return;

        foreach (Transform slot in cannonSlots)
        {
            Instantiate(cannonPrefab, slot.position, slot.rotation, slot);
        }
    }

    private void EquipSails(GameObject sailPrefab)
    {
        if (sailPrefab == null || sailSlots == null || sailSlots.Length == 0)
            return;

        foreach (Transform slot in sailSlots)
        {
            Instantiate(sailPrefab, slot.position, slot.rotation, slot);
        }
    }

    private void ApplyRandomTexture()
    {
        if (availableTextures == null || availableTextures.Length == 0) return;
        if (shipMeshRenderers == null || shipMeshRenderers.Length == 0) return;

        int randomIndex = Random.Range(0, availableTextures.Length);
        Texture2D selectedTexture = availableTextures[randomIndex];

        foreach (MeshRenderer renderer in shipMeshRenderers)
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetTexture("_MainTex", selectedTexture);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
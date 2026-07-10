using UnityEngine;
using System.Collections.Generic;

public class ShipSockets : MonoBehaviour
{
    [Header("Mount Points")]
    [Tooltip("Geminin üzerindeki top yuvalarý.")]
    public List<Transform> cannonSockets;

    [Tooltip("Geminin üzerindeki yelken direði yuvalarý.")]
    public List<Transform> sailSockets;

    [Header("Scale Settings")]
    [Tooltip("Bu gemi için baz alýnacak referans boyut (Örn: 1,1,1)")]
    public Vector3 referenceShipScale = new Vector3(1f, 1f, 1f);
}
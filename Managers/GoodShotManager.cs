using UnityEngine;

public class GoodShotManager : MonoBehaviour
{
    [Header("Cannon Settings")]
    [SerializeField] private float[] _perfectShotThresholds;

    [SerializeField] private ShipSpawner _shipSpawner;

    [SerializeField] private GoodShotUI _goodShotUI;

    private int _cannonID;

    private void OnEnable()
    {
        CannonBall.OnBallHitTarget += AnalyzeShot;
        EntryPanel.onStartGame += GetCannonID;
    }

   
    private void OnDisable()
    {
        CannonBall.OnBallHitTarget -= AnalyzeShot;
        EntryPanel.onStartGame -= GetCannonID;
    }



    private void AnalyzeShot(float distance)
    {
        float requiredDistance = (_cannonID < _perfectShotThresholds.Length) ? _perfectShotThresholds[_cannonID] : 14f;

        if (distance > requiredDistance)
        {
            _goodShotUI.PlayerDoGoodShot(distance);

        }

    }

    private void GetCannonID()
    {
        _cannonID=_shipSpawner.currentCannonID;
    }
}
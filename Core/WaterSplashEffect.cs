using UnityEngine;
using System.Collections;

public class WaterSplashEffect : MonoBehaviour
{
    [SerializeField] private float _effectDuration = 2.0f; 

    private void OnEnable()
    {

        StartCoroutine(DeactivateAfterDelay(_effectDuration));
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); 
    }
}
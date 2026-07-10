using System.Collections;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI fpsText;

    [Header("Debug Settings")]
    [Tooltip("Test amaçlý Inspector üzerinden sayacý açýp kapatmak için kullanýn.")]
    public bool showFps;

    private bool _isFpsCounterActive;
    private Coroutine _fpsCoroutine;

    private void Update()
    {

        if (showFps != _isFpsCounterActive)
        {
            ToggleFpsCounter(showFps);
        }
    }

    private void ToggleFpsCounter(bool isActive)
    {
        _isFpsCounterActive = isActive;

        if (_isFpsCounterActive)
        {
            if (fpsText != null) fpsText.gameObject.SetActive(true);

            if (_fpsCoroutine != null) StopCoroutine(_fpsCoroutine);
            _fpsCoroutine = StartCoroutine(UpdateFpsCounterEverySecond());
        }
        else
        {
            if (_fpsCoroutine != null)
            {
                StopCoroutine(_fpsCoroutine);
                _fpsCoroutine = null;
            }
            if (fpsText != null) fpsText.gameObject.SetActive(false);
        }
    }

    private IEnumerator UpdateFpsCounterEverySecond()
    {
        while (_isFpsCounterActive)
        {
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;

            yield return new WaitForSecondsRealtime(1.0f);

            float timePassed = Time.realtimeSinceStartup - lastTime;
            int framesRendered = Time.frameCount - lastFrameCount;

            int currentFps = Mathf.RoundToInt(framesRendered / timePassed);

            if (fpsText != null)
            {
                fpsText.text = $"FPS: {currentFps}";
            }
        }
    }
}
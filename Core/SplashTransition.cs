using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image logoImage;

    [Header("Timings")]
    [SerializeField] private float fadeInTime = 0.35f;
    [SerializeField] private float introTime = 0.85f;
    [SerializeField] private float holdTime = 0.25f;
    [SerializeField] private float outroTime = 0.35f;
    [SerializeField] private float fadeOutTime = 0.30f;

    [Header("Target Scene")]
    [SerializeField] private string targetScene = "SampleScene";

    private void Start()
    {
        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        // Reset
        canvasGroup.alpha = 0f;
        SetLogoAlpha(0f);
        logoImage.rectTransform.localScale = Vector3.one * 0.72f;

        //  before fade in 
        yield return FadeCanvas(0f, 1f, fadeInTime);

        // then logo intro
        yield return LogoIntro();

        // hold and  async load
        var op = SceneManager.LoadSceneAsync(targetScene);
        op.allowSceneActivation = false;

        yield return WaitUnscaled(holdTime);

        while (op.progress < 0.9f)
            yield return null;

        //  logo outro
        yield return LogoOutro();

        op.allowSceneActivation = true;
        yield return null;

        //  fade out 
        yield return FadeCanvas(1f, 0f, fadeOutTime);
    }

    private IEnumerator LogoIntro()
    {
        float t = 0f;
        while (t < introTime)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / introTime);
            float s = EaseOutBack(p);

            logoImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(0.72f, 1.02f, s);
            SetLogoAlpha(Mathf.SmoothStep(0f, 1f, p));
            yield return null;
        }

        yield return ScaleTo(1.02f, 1.00f, 0.12f);
        yield return AlphaPulse(0.85f, 0.10f);
    }

    private IEnumerator LogoOutro()
    {
        float startScale = logoImage.rectTransform.localScale.x;
        float startA = logoImage.color.a;
        float t = 0f;

        while (t < outroTime)
        {
            t += Time.unscaledDeltaTime;
            float ease = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / outroTime));
            logoImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(startScale, 0.80f, ease);
            SetLogoAlpha(Mathf.Lerp(startA, 0f, ease));
            yield return null;
        }
        SetLogoAlpha(0f);
    }

    private IEnumerator FadeCanvas(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / duration)));
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    private IEnumerator ScaleTo(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            logoImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(from, to, Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / duration)));
            yield return null;
        }
        logoImage.rectTransform.localScale = Vector3.one * to;
    }

    private IEnumerator AlphaPulse(float targetAlpha, float halfDuration)
    {
        float a0 = logoImage.color.a;
        yield return AlphaTo(a0, targetAlpha, halfDuration);
        yield return AlphaTo(targetAlpha, 1f, halfDuration);
    }

    private IEnumerator AlphaTo(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            SetLogoAlpha(Mathf.Lerp(from, to, Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / duration))));
            yield return null;
        }
        SetLogoAlpha(to);
    }

    private IEnumerator WaitUnscaled(float seconds)
    {
        float t = 0f;
        while (t < seconds) { t += Time.unscaledDeltaTime; yield return null; }
    }

    private void SetLogoAlpha(float a) =>
        logoImage.color = new Color(1f, 1f, 1f, a);

    private float EaseOutBack(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(x - 1f, 3f) + c1 * Mathf.Pow(x - 1f, 2f);
    }
}
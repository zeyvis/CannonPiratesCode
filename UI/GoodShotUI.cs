using System.Collections;
using TMPro;
using UnityEngine;

public class GoodShotUI : MonoBehaviour
{
    [SerializeField] private GameObject _goodShotImage;
    [SerializeField] private TextMeshProUGUI _shotDistanceText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _hitMarkerSoundEffect;

    private float _shotDistance;

    public void PlayerDoGoodShot(float distance)
    {
        _shotDistance = distance;
        ShowImage();
        PlaySoundEffect();
    }
    
    private void PlaySoundEffect()
    {
        _audioSource.PlayOneShot(_hitMarkerSoundEffect);
    }
    private void ShowImage()
    {
        _shotDistanceText.text = _shotDistance.ToString("F2") + "m";
        StartCoroutine(ShowImageRoutine());
    }

    private IEnumerator ShowImageRoutine()
    {
        _goodShotImage.SetActive(true);

        yield return new WaitForSeconds(1.6f);

        _goodShotImage.SetActive(false);
    }
}
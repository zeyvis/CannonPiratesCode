using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    [SerializeField] private AdsManager _adsManager;



    public void PlayRewardedAds()
    {
        _adsManager.ShowRewardedAd();
    }

    private void PlayInterstitialAd()
    {
        _adsManager.ShowInterstitialAd();
    }

   

    public void OnEnable()
    {
        ShipHealth.OnPlayerDeath += PlayInterstitialAd;
        ContinueButton.OnContinueGame += PlayRewardedAds;
    }
    public void OnDisable()
    {
        ShipHealth.OnPlayerDeath -= PlayInterstitialAd;
        ContinueButton.OnContinueGame -= PlayRewardedAds;
    }
}

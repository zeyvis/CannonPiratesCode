using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    public void InitializeAds()
    {

        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob baþlatýldý.");
            LoadInterstitialAd();
            LoadRewardedAd();
        });
    }
    

   

    public void LoadInterstitialAd()
    {
       
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        string adUnitId = "ca-app-pub-9190045623570547/5713743713"; 
        AdRequest adRequest = new AdRequest();

        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Geçiþ reklamý yüklenemedi: " + error);
                return;
            }

            interstitialAd = ad;
            Debug.Log("Geçiþ reklamý yüklendi.");

           
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Geçiþ reklamý kapatýldý.");
                AudioListener.pause = false; 
                LoadInterstitialAd();       
            };
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            AudioListener.pause = true; 
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Geçiþ reklamý henüz hazýr deðil.");
        }
    }


   

    public void LoadRewardedAd()
    {
        
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        string adUnitId = "ca-app-pub-9190045623570547/9461417036"; 
        AdRequest adRequest = new AdRequest();

        RewardedAd.Load(adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Ödüllü reklam yüklenemedi: " + error);
                return;
            }

            rewardedAd = ad;
            Debug.Log("Ödüllü reklam yüklendi.");

           
            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Ödüllü reklam kapatýldý.");
                AudioListener.pause = false; 
                LoadRewardedAd();           
            };
        });
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            AudioListener.pause = true; 

            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("Kullanýcý ödül kazandý: " + reward.Amount);
               
            });
        }
        else
        {
            Debug.Log("Ödüllü reklam henüz hazýr deðil.");
        }
    }
}
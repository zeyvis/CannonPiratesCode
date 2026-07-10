using GoogleMobileAds.Ump.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsentManager : MonoBehaviour
{
    public AdsManager adsManager; 

    void Start()
    {
        RequestConsent();
    }

    void RequestConsent()
    {
        ConsentRequestParameters requestParameters = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false 
        };

        ConsentInformation.Update(requestParameters, (FormError formError) =>
        {
            if (formError != null)
            {
                Debug.LogError("UMP güncelleme hatasý: " + formError.Message);
                adsManager.InitializeAds();
                return;
            }

            if (ConsentInformation.CanRequestAds())
            {
                LoadConsentForm();
            }
            else
            {
                adsManager.InitializeAds();
            }
        });
    }

    void LoadConsentForm()
    {
        ConsentForm.Load((ConsentForm form, FormError loadError) =>
        {
            if (loadError != null)
            {
                Debug.LogError("Ýzin formu yükleme hatasý: " + loadError.Message);
                adsManager.InitializeAds();
                return;
            }

            if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
            {
                form.Show((FormError showError) =>
                {
                    if (showError != null)
                    {
                        Debug.LogError("Form gösterme hatasý: " + showError.Message);
                        adsManager.InitializeAds();
                        return;
                    }

                    if (ConsentInformation.CanRequestAds())
                    {
                        adsManager.InitializeAds();
                    }
                });
            }
            else
            {
                adsManager.InitializeAds();
            }
        });
    }
}

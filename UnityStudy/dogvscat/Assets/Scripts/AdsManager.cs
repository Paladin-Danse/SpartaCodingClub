using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    InterstitialAd Inter_ad;
    AdsInitializer adsInitializer;

    private void Start()
    {
        Inter_ad = GetComponent<InterstitialAd>();
        adsInitializer = GetComponent<AdsInitializer>();
    }

    public void loadNshowAds()
    {
        if (!Advertisement.isShowing)
        {
            Inter_ad.InvokeAd();
        }
    }

}
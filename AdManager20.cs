using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;
using System;
//using GoogleMobileAdsMediationTestSuite.Api;


public class AdManager20 : MonoBehaviour
{

    // Use this for initialization
    public int RewardEuroi;
    public static bool IsShownBanner = false;
    private static BannerView bannerView;
    public static bool ShouldBannerBeOn = true;
    private static InterstitialAd interstitial;
    public static RewardBasedVideoAd rewardBasedVideo;
    const bool test = true;
    //private static bool isStarted = false;
    private static bool rewardBasedEventHandlersSet;


    //banner ca-app-pub-1411129145395601/7558259506
    //interstitial ca-app-pub-1411129145395601/3591094336
    //video ca-app-pub-1411129145395601/7038468457
    public static void Start()
    {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-1411129145395601~2525424674";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif
        if(test)
            adUnitId = "ca-app-pub-3940256099942544/5224354917";

        Debug.Log("Test Mediation");
        //MediationTestSuite.Show(adUnitId);
        //MediationTestSuite.OnMediationTestSuiteDismissed += MediationTestSuite_OnMediationTestSuiteDismissed;
        //return;
        MobileAds.Initialize(adUnitId);
       
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        if (!rewardBasedEventHandlersSet)
        {
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
            rewardBasedEventHandlersSet = true;
        }

        //RequestBannner();
        //RequestInterstitial();
        //RequestRewardedVideo();
        //ShowRewardedVideo();
    }

    private static void MediationTestSuite_OnMediationTestSuiteDismissed(object sender, EventArgs e)
    {
        Debug.Log("Failed Mediation test");
    }

    private static AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
    }

    public static void RequestBannner(AdSize adSize, AdPosition adPosition)
    {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-1411129145395601/7558259506";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (test)
            adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //if (IsShownBanner)
        //    return;
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        IsShownBanner = true;
        bannerView = new BannerView(adUnitId, adSize, adPosition);

        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;

        bannerView.LoadAd(CreateAdRequest());
        //bannerView.Show();
        Debug.Log("banner");
    }
    public static bool ShowBanner()
    {
        if (bannerView != null) {
            bannerView.Show();
            return true;
        }
        return false;
    }
    public static void RequestInterstitial()
    {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-1411129145395601/3591094336";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (test)
            adUnitId = "	ca-app-pub-3940256099942544/1033173712";
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;

        interstitial.LoadAd(CreateAdRequest());
    }

    public static void RequestRewardedVideo()
    {

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-1411129145395601/7038468457";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif
        if (test)
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        rewardBasedVideo.LoadAd(CreateAdRequest(), adUnitId);
        Debug.Log("Requested rewarded Video");
    }

    public static void ShowRewardedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
            rewardBasedVideo.Show();
        else
            rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
            Debug.Log("Reward based video ad is not ready yet");
    }

    private static void RewardBasedVideo_OnAdLoaded(object sender, EventArgs e)
    {
        rewardBasedVideo.Show();
        rewardBasedVideo.OnAdLoaded -= RewardBasedVideo_OnAdLoaded;
    }

    public static void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
        else
            interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
            Debug.Log("Interstitial is not ready yet");

    }

    private static void Interstitial_OnAdLoaded(object sender, EventArgs e)
    {
        interstitial.Show();
        interstitial.OnAdLoaded -= Interstitial_OnAdLoaded;
    }

    public static void HideBanner()
    {
        try
        {
            bannerView.Hide();
            IsShownBanner = false;
        }
        catch { }
    }

    #region Banner callback handlers

    public static void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
        if (ShouldBannerBeOn)
            bannerView.Show();
        else
            bannerView.Destroy();

    }

    public static void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public static void HandleAdOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleAdOpened event received");
    }

    public static void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleAdClosed event received");
    }

    public static void HandleAdLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public static void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialLoaded event received");
    }

    public static void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public static void HandleInterstitialOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialOpened event received");
    }

    public static void HandleInterstitialClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialClosed event received");
    }

    public static void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public static void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoLoaded event received");
        // ShowRewardedVideo();
    }

    public static void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public static void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoOpened event received");
    }

    public static void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoStarted event received");

    }

    public static void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoClosed event received");
        RequestRewardedVideo();
    }

    public static void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {

        PlayerPrefs.SetInt("euroi", PlayerPrefs.GetInt("euroi", 0) + 2000);
        string type = args.Type;
        int amount = Convert.ToInt32(args.Amount);


        //PlayerPrefs.SetInt("euroi", PlayerPrefs.GetInt("euroi",0) + amount);
        PlayerPrefs.SetString("cooldown", DateTimeOffset.Now.AddHours(4).Ticks.ToString());
        Debug.Log("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);


    }
    public static void DestroyBanner()
    {
        try
        {
            bannerView.Destroy();
        }
        catch { }
    }
    public static void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}

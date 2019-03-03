using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using GoogleMobileAds.Api;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class StartScriptMenu : MonoBehaviour
{

    //public Animation Anim;
    public GameObject Negru;
    public GameObject Warning;
    public Button buttonCastigaEuroi;
    public Button buttonLeader;
    public Text textboxEuroi;
    public Text textboxCooldown;
    public Text textboxConexiune;
    public Text textboxLogIn;
    public Text textBoxAuthenticated;
    public Image RoseataCastigaEuroi;
    public GameObject PanelVizionare;
    public static bool IsPanouWarningTriggered = false;


    TimeSpan cooldownTime;
    DateTimeOffset cooldown;
    int euroi;

    AdManager20 adManager;

    float timeLeft;
    //bool VideoRequested = false;
    public void Start()
    {
        AdManager20.Start();
        //return;
        

        if (!IsPanouWarningTriggered)
        {
            Debug.Log(1);
            Negru.SetActive(true);
            Warning.SetActive(true);
            //Destroy(Warning, 2);
            //Destroy(Negru, 6);
            Invoke("PauseAnimation", 2);

            IsPanouWarningTriggered = true;
        }
        else
        {
            if (!AdManager20.ShowBanner())
                AdManager20.RequestBannner(AdSize.Banner, AdPosition.Bottom);
        }
        long clickTime = long.Parse(PlayerPrefs.GetString("cooldown", "0"));
        if (clickTime > 0)
        {
            cooldown = new DateTime(clickTime);
            cooldownTime = cooldown.Subtract(DateTimeOffset.Now);
            if (cooldownTime.TotalSeconds <= 0)
            {
                //AdManager20.RequestRewardedVideo();
            }

        }
        else
        {
            //AdManager20.RequestRewardedVideo();
        }
        //AdManager20.RequestBannner(AdSize.SmartBanner, AdPosition.Bottom);
        PanelVizionare.SetActive(false);
        textboxEuroi.text = PlayerPrefs.GetInt("euroi", 0).ToString();
        textboxCooldown.text = "";
        RoseataCastigaEuroi.enabled = false;
        textboxConexiune.enabled = false;

        if (!Social.localUser.authenticated)
        {
            StartGooglePlayServices();
            FirstSignIn();
        }
        //PlayerPrefs.SetString("cooldown", "0");
        //PlayerPrefs.SetInt("euroi", 9999999);

    }

    private void FirstSignIn()
    {


        if(PlayerPrefs.GetString("FirstSignIn","yes")=="yes")
        {
            PlayerPrefs.SetString("FirstSignIn", "no");
            Debug.Log("NO");
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
        }
        else
        {
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
        }
    }

    private void StartGooglePlayServices()
    {

        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(Lollygagger) Signed in!");
            textboxLogIn.text = "Delogheaza-te";
            textBoxAuthenticated.text = "Bine ai venit, " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("(Lollygagger) Sign-in failed...");
            textBoxAuthenticated.text = "";
            textboxLogIn.text = "Logheaza-teee";
        }
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        else
        {
            PlayGamesPlatform.Instance.SignOut();
            textboxLogIn.text = "Logheaza-te";
            textBoxAuthenticated.text = "";
        }
    }
    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    // Use this for initialization

    public void Update()
    {
        textboxEuroi.text = PlayerPrefs.GetInt("euroi", 0).ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        long clickTime = long.Parse(PlayerPrefs.GetString("cooldown", "0"));
        if (clickTime > 0)
        {
            cooldown = new DateTime(clickTime);
            cooldownTime = cooldown.Subtract(DateTimeOffset.Now);

            // DateTimeOffset universal = DateTimeOffset.Now.ToUniversalTime();

            if (cooldownTime.TotalSeconds > 0)
            {
                textboxCooldown.text = cooldownTime.Hours + ":" + cooldownTime.Minutes.ToString("00") + ":" + cooldownTime.Seconds.ToString("00");
                textboxCooldown.enabled = true;
                RoseataCastigaEuroi.enabled = true;
                buttonCastigaEuroi.enabled = false;
            }
            else
            {
                PlayerPrefs.SetString("cooldown", "0");
                textboxCooldown.enabled = false;
                RoseataCastigaEuroi.enabled = false;
                buttonCastigaEuroi.enabled = true;
            }
        }
        try
        {
            buttonLeader.gameObject.SetActive(Social.localUser.authenticated);
        }
        catch (Exception) { }
    }

    public void ButtonJoaca_OnClick()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("SelectGameMode");

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void IntraMagazin()
    {
        AdManager20.HideBanner();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Magazin");
    }

    public void ButtonCastigaEuroi_OnClick()
    {
        //PlayerPrefs.SetString("DataClick", DateTimeOffset.Now.Ticks.ToString());
        //TimeSpan cooldownTime = cooldown - dataClick;
        AdManager20.RequestRewardedVideo();
        PanelVizionare.SetActive(!PanelVizionare.activeInHierarchy);
    }

    private void RewardBasedVideo_OnAdLoaded(object sender, EventArgs e)
    {
        ((RewardBasedVideoAd)sender).OnAdLoaded -= RewardBasedVideo_OnAdLoaded;
        AdManager20.ShowRewardedVideo();
    }

    public void ButtonVizionare_OnClick()
    {
        if (IsConectedToInternet())
        {
            AdManager20.HideBanner();
            PanelVizionare.SetActive(false);
            //AdManager20.rewardBasedVideo.OnAdLoaded += RewardBasedVideo_OnAdLoaded;
            if (AdManager20.rewardBasedVideo.IsLoaded())
                AdManager20.ShowRewardedVideo();
            else
            {
                StartCoroutine(CheckLoadingAd());
            }
            //buttonCastigaEuroi.enabled = false;

            textboxConexiune.enabled = false;
        }
        else
        {
            textboxConexiune.enabled = true;
        }
    }
    IEnumerator CheckLoadingAd()
    {
        while (!AdManager20.rewardBasedVideo.IsLoaded())
        {
            yield return new WaitForSeconds(0.1f);
        }
        AdManager20.ShowRewardedVideo();
        Debug.Log("finished");
    }
    bool IsConectedToInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
            return true;
        /*
        WWW www = new WWW("http://www.google.com");
        if (www.error != null)
            return false;
        else
            return true;
            */
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("PanelNegru", 0);
    }
    IEnumerator BannerAfterSecs(float time)
    {
        yield return new WaitForSeconds(time);

        AdManager20.RequestBannner(AdSize.Banner, AdPosition.Bottom);
    }
    void BannerAfter5()
    {
        AdManager20.ShowBanner();
    }
    void PauseAnimation()
    {
        Negru.GetComponent<Animator>().speed = 0;
        Negru.GetComponent<Image>().enabled = false;
    }
    public void ResumeAnimation()
    {
        Negru.GetComponent<Image>().enabled = true;
        Negru.GetComponent<Animator>().speed = 1;
        Negru.GetComponent<Image>().raycastTarget = true;
        Destroy(Warning, 1);
        Destroy(Negru, 2.50f);
        AdManager20.RequestBannner(AdSize.Banner, AdPosition.Bottom);
    }
}

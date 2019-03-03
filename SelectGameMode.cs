using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class SelectGameMode : MonoBehaviour
{

    // Use this for initialization

    public GameObject PanelHardCore;
    public Button ButtonFonduri;
    public Button ButtonDa;
    public Text textFonduri;
    public Text textDa;
    public GameObject PanelNegru;
    void Start()
    {
        //AdManager20.RequestBannner(AdSize.SmartBanner, AdPosition.Bottom);
        Debug.Log(PlayerPrefs.GetInt("euroi"));
        //PanelHardCore.SetActive(false);

        if (PlayerPrefs.GetInt("euroi") < 400)
        {
            ButtonFonduri.image.enabled = true;
            textFonduri.enabled = true;

            ButtonDa.image.enabled = false;
            textDa.enabled = false;
        }
        else
        {
            ButtonFonduri.image.enabled = false;
            textFonduri.enabled = false;

            ButtonDa.image.enabled = true;
            textDa.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }

    public void buttonTimeAttack_OnClick()
    {
        GameModes.TimeAttack = true;
        GameModes.Hardcore = false;

        SceneManager.LoadScene("LoadingScreen");

        AdManager20.HideBanner();
  
    }
    public void ButtonExit_OnClick()
    {
        SceneManager.LoadScene("Menu");
    }
    public void buttonHardcore_OnClick()
    {
        PanelNegru.SetActive(true);
    }

    public void ButtonDa_OnClick()
    {
        PlayerPrefs.SetInt("euroi", PlayerPrefs.GetInt("euroi") - 400);
        GameModes.Hardcore = true;
        GameModes.TimeAttack = false;

        SceneManager.LoadScene("LoadingScreen");

        AdManager20.HideBanner();
    }

    public void ButtonInapoi_OnClick()
    {
        PanelNegru.SetActive(false);
    }

    public void buttonNormal_OnClick()
    {
        GameModes.TimeAttack = false;
        GameModes.Hardcore = false;

        SceneManager.LoadScene("LoadingScreen");

        AdManager20.HideBanner();
    }

    public void PanelNegru_OnClick()
    {
        PanelNegru.SetActive(false);
    }
}

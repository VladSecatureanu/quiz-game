using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using GoogleMobileAds.Api;
public class GameOver : MonoBehaviour
{

    // Use this for initialization
    public int HighScore;
    public Text textBoxHighScore;
    public int Score;
    public Text textboxScore;
    public Text textboxEuroi;
    public Text textboxGamemode;
    public GameObject panel;
    public Sprite fundalFaraTimp;
    int euroi;

    public void Start()
    {
        AdManager20.RequestBannner(AdSize.Banner, AdPosition.Bottom);

        if (GameModes.TimeAttack)
        {
            textBoxHighScore.text = PlayerPrefs.GetInt("HighScoreTimeattack").ToString();
            textboxScore.text = PlayerPrefs.GetInt("ScoreTimeattack").ToString();
            textboxGamemode.text = "(Time Attack)";
            //panel.GetComponent<Image>().sprite = fundalFaraTimp;
        }
        else if (GameModes.Hardcore)
        {
            textBoxHighScore.text = PlayerPrefs.GetInt("HighScoreHardcore").ToString();
            textboxScore.text = PlayerPrefs.GetInt("ScoreHardcore").ToString();
            textboxGamemode.text = "(Hardcore)";
        }
        else
        {
            textBoxHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            textboxScore.text = PlayerPrefs.GetInt("Score").ToString();
            textboxGamemode.text = "(Clasic)";
        }

        textboxEuroi.text = PlayerPrefs.GetInt("castig").ToString();
        
        if(PlayerPrefs.GetInt("FaraTimp",0)==1)
        {
            panel.GetComponent<Image>().sprite = fundalFaraTimp;
        }
        //euroi = PlayerPrefs.GetInt("euroi");
        //PlayerPrefs.SetInt("euroiinitial", euroi);

    }
  
    public void StartMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu");
    }
    public void Iesi()
    {

    }
    void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("Menu");
        }
    }

}

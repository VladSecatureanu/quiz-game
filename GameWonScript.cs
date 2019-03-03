using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


public class GameWonScript : MonoBehaviour {

    public Text textboxScore;
    public Text textboxEuroi;
    public Text textboxGamemode;
    int euroi;

    public void Start()
    {
        if (GameModes.Hardcore)
        {
            textboxScore.text = PlayerPrefs.GetInt("ScoreHardcore").ToString();
            textboxGamemode.text = "(Hardcore)";
        }
        else
        {
            textboxScore.text = PlayerPrefs.GetInt("Score").ToString();
            textboxGamemode.text = "(Clasic)";
        }
        textboxEuroi.text = PlayerPrefs.GetInt("castig").ToString();
        // euroi = PlayerPrefs.GetInt("euroi");
        //PlayerPrefs.SetInt("euroiinitial", euroi);
    }

    public void StartMenu()
    {
        //SceneManager.UnloadScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }
}

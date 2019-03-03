using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magazin : MonoBehaviour
{

    public Text textEuroi;
    public int pretDeBaza;
    public int pretBineAiVenit;
    public Button pachetBineAiVenit;
    public Image bineVenit;

    int euroi;
    int pauze;
    int juma;
    int Mafia;
    int Skip;

    int pretPauza;
    int pretJuma;
    int pretSkip;
    int pretMafia;


    // Use this for initialization
    void Start()
    {
       //PlayerPrefs.SetInt("euroi", 10000);

        euroi = PlayerPrefs.GetInt("euroi");
        pauze = PlayerPrefs.GetInt("pauze");
        juma = PlayerPrefs.GetInt("juma");
        Mafia = PlayerPrefs.GetInt("Mafia");
        Skip = PlayerPrefs.GetInt("Skip");

        pretPauza = Convert.ToInt32(2 * pretDeBaza);
        pretMafia = Convert.ToInt32(8 * pretDeBaza);
        pretJuma = Convert.ToInt32(7 * pretDeBaza);
        pretSkip = Convert.ToInt32(4 * pretDeBaza);

        if (PlayerPrefs.GetInt("binevenit") == 2)
        {
            pachetBineAiVenit.enabled = false;
            bineVenit.enabled = true;
        }
        else
        {
            pachetBineAiVenit.enabled = true;
            bineVenit.enabled = false;
        }
        
        AdManager20.HideBanner();
    }

    // Update is called once per frame
    void Update()
    {
        textEuroi.text = (PlayerPrefs.GetInt("euroi")).ToString() + " ";

        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("Menu");
        }
    }

    public void UserBuy(int optiune)
    {

        if (optiune == 1)
        {
            if (euroi >= pretPauza)
            {
                pauze++;
                euroi -= pretPauza;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("pauze", pauze);
                //CommandMoneyRain.numarValoare(pretPauza);
            }
        }

        else if (optiune == 2)
        {
            if (euroi >= 4 * pretPauza)
            {
                pauze += 5;
                euroi -= 4 * pretPauza;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("pauze", pauze);
                //CommandMoneyRain.numarValoare(4 * pretPauza);
            }
        }

        else if (optiune == 3)
        {
            if (euroi >= 8 * pretPauza)
            {
                pauze += 10;
                euroi -= 8 * pretPauza;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("pauze", pauze);
                //CommandMoneyRain.numarValoare(8 * pretPauza);
            }
        }

        else if (optiune == 4)
        {
            if (euroi >= pretMafia)
            {
                Mafia++;
                euroi -= pretMafia;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("Mafia", Mafia);
                //CommandMoneyRain.numarValoare(pretMafia);
            }
        }

        else if (optiune == 5)
        {
            if (euroi >= 4 * pretMafia)
            {
                Mafia += 5;
                euroi -= 4 * pretMafia;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("Mafia", Mafia);

                //CommandMoneyRain.numarValoare(4 * pretMafia);
            }
        }

        else if (optiune == 6)
        {
            if (euroi >= 8 * pretMafia)
            {
                Mafia += 10;
                euroi -= 8 * pretMafia;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("Mafia", Mafia);
                CommandMoneyRain.numarValoare(8 * pretMafia);
            }
        }

        else if (optiune == 7)
        {
            if (euroi >= pretJuma)
            {
                juma++;
                euroi -= pretJuma;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("juma", juma);
                //CommandMoneyRain.numarValoare(pretJuma);
            }
        }

        else if (optiune == 8)
        {
            if (euroi >= 4 * pretJuma)
            {
                juma += 5;
                euroi -= 4 * pretJuma;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("juma", juma);
                //CommandMoneyRain.numarValoare(pretJuma*4);
            }
        }

        else if (optiune == 9)
        {
            if (euroi >= 8 * pretJuma)
            {
                juma += 10;
                euroi -= 8 * pretJuma;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("juma", juma);
                //CommandMoneyRain.numarValoare(pretJuma*8);
            }
        }

        else if (optiune == 10)
        {
            if (euroi >= pretSkip)
            {
                Skip++;
                euroi -= pretSkip;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("Skip", Skip);

                //CommandMoneyRain.numarValoare(pretSkip);
            }
        }

        else if (optiune == 11)
        {
            if (euroi >= 4 * pretSkip)
            {
                Skip += 5;
                euroi -= 4 * pretSkip;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("Skip", Skip);
                //CommandMoneyRain.numarValoare(pretSkip * 4);
            }
        }

        else if (optiune == 12)
        {
            if (euroi >= 8 * pretSkip)
            {
                Skip += 10;
                euroi -= 8 * pretSkip;
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("Skip", Skip);
                //CommandMoneyRain.numarValoare(pretSkip * 8);
            }
        }

        if (optiune == 13)
        {
            if (euroi >= pretBineAiVenit)
            {
                pauze += 20;
                Skip += 15;
                juma += 10;
                Mafia += 5;
                PlayerPrefs.SetInt("binevenit", 2);
                euroi -= pretBineAiVenit;
                PlayerPrefs.SetInt("Skip", Skip);
                PlayerPrefs.SetInt("juma", juma);
                PlayerPrefs.SetInt("Mafia", Mafia);
                PlayerPrefs.SetInt("euroi", euroi);
                PlayerPrefs.SetInt("pauze", pauze);

                //CommandMoneyRain.numarValoare(pretBineAiVenit);

                pachetBineAiVenit.enabled = false;
                bineVenit.enabled = true;
            }
        }
        /*
        PlayerPrefs.SetInt("euroi", euroi);
        PlayerPrefs.SetInt("Skip", Skip);
        PlayerPrefs.SetInt("juma", juma);
        PlayerPrefs.SetInt("Mafia", Mafia);
        PlayerPrefs.SetInt("pauze", pauze);
        */

    }

    public void InpapoiLaMeniu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu");
    }
}

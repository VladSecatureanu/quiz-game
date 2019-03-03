using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using GoogleMobileAds.Api;
using System.IO;

public class Creier : MonoBehaviour
{

    List<int> k;
    public List<Question> Haos;
    #region Resources
    public Text numarPauze;
    public Text numar5050;
    public Text numarMafia;
    public Text numarSkip;
    public GameObject mafiaRosu;
    public GameObject skipRosu;
    public GameObject fiftyRosu;
    public GameObject fundalPauzaRosie;
    public Text ScoreText;
    public Text TimpText;
    public Text[] Variante = new Text[4];
    public Text Intrebare;
    public Text RaspunsMafiot;
    public Text textEuroi;
    public Text textEuroiCastigati;
    public Text textTimpPierdut;
    public Sprite fundalGalben;
    public Sprite fundalVerde;
    public Sprite fundalRosu;
    public AudioSource bineMaaa;
    public AudioSource sunetRau;
    public GameObject CreierFinal;
    public Button[] Buttons = new Button[4];
    public Image[] Bani = new Image[3];
    public GameObject panelPauza;
    public Button buttonPauza;
    public Button button5050;
    public Button ButtonMafia;
    public Image Mafiot;
    public Image Bula;
    public Image[] ImaginiEuroi = new Image[4];
    public GameObject panelIesire;
    public GameObject panelInimoare;
    public GameObject panelNegru;
    public GameObject panelCareFixeazaUnFeature;
    #endregion

    public AudioSource[] cantece;


    public int HighScore;
    private int Score, Vietzi;
    public int timpPentruPauza = 2;
    public float timpRamas;
    public float timp = 0;
    public float pauza = 0;

    public int ConstantaCost;
    public int cost5050;
    public int costPause;
    public int costMafiot;
    public int costSkip;

    public int difficultyRamp;
    private float timpPentruIntrebare = 30.5f;
    public float timpPentruIntrebareTimeAttack = 60.5f;
    //private int counter;
    int HaosLength;
    int euroi;
    int indexIntrebareCurenta;
    bool isGameOver = false;
    float timpScurs;
    
    public int[] dificultyIncreaseScores = new int[] { 25, 50, 75, 100, 115 };
    int timeAttackPenalty = 5;

    public int MultiplierHardCore;
    public int MultiplierTimeAttack;



    void Load()
    {
        cantece = Resources.LoadAll<AudioSource>("Runda 3 De Manele");
        foreach (AudioSource clip in cantece)
        {
            Debug.Log(clip.name);
        }
    }

    void Start()
    {
        //Load();
        //AdManager20.HideBanner();
        AdManager20.ShouldBannerBeOn = false;
        AdManager20.DestroyBanner();
        AdManager20.RequestInterstitial();
        //CheckDuplicateQuestions();
        Initializiaza();
        Debug.Log(Haos.Count);
        //indexIntrebareCurenta = 155;
        AmestecareIntrebari();
        GetNextQuestion();

        panelCareFixeazaUnFeature.SetActive(false);
        textEuroi.text = euroi.ToString();
        //textEuroi.text = GameModes.TimeAttack.ToString() + " " + GameModes.Hardcore.ToString();

    }

    private void CheckDuplicateQuestions()
    {
        List<string> debug = new List<string>();
        for (int i = 0; i < Haos.Count; i++)
        {
            //Debug.Log(i); 
            if (debug.Contains(Haos[i].Raspuns))
                Debug.Log(Haos[i].Raspuns + " " + i);
            debug.Add(Haos[i].Raspuns);
        }
    }

    private void Initializiaza()
    {
        /*
        for (int i = 0; i < Haos.Count; i++)
        {
            Haos[i].Intreb =
            Haos[i].Raspuns = i.ToString();
        }
        */

        costPause = 2 * ConstantaCost;
        cost5050 = 7 * ConstantaCost;
        costSkip = 4 * ConstantaCost;
        costMafiot = 8 * ConstantaCost;

        PlayerPrefs.SetInt("castig", 0);

        if (PlayerPrefs.GetInt("pauze") > 0)
        {
            numarPauze.text = PlayerPrefs.GetInt("pauze").ToString();
            ImaginiEuroi[0].enabled = false;

        }
        else
        {
            numarPauze.text = (costPause).ToString();

        }

        if (PlayerPrefs.GetInt("juma") > 0)
        {
            numar5050.text = PlayerPrefs.GetInt("juma").ToString();
            ImaginiEuroi[1].enabled = false;
        }
        else
        {
            numar5050.text = (cost5050).ToString();

        }

        if (PlayerPrefs.GetInt("Mafia") > 0)
        {
            numarMafia.text = PlayerPrefs.GetInt("Mafia").ToString();
            ImaginiEuroi[3].enabled = false;
        }
        else
        {
            numarMafia.text = (costMafiot).ToString();
        }

        if (PlayerPrefs.GetInt("Skip") > 0)
        {
            numarSkip.text = PlayerPrefs.GetInt("Skip").ToString();
            ImaginiEuroi[2].enabled = false;

        }
        else
        {
            numarSkip.text = (costSkip).ToString();
        }

        if (GameModes.Hardcore)
        {
            Vietzi = 1;
            Bani[0].enabled = false;
            Bani[2].enabled = false;
        }
        else if (GameModes.TimeAttack)
        {
            timpRamas = timpPentruIntrebareTimeAttack;
            panelInimoare.SetActive(false);
        }

        mafiaRosu.SetActive(false);
        skipRosu.SetActive(false);
        fiftyRosu.SetActive(false);
        fundalPauzaRosie.SetActive(false);


        euroi = PlayerPrefs.GetInt("euroi");
        //timpRamas = 30.5f;
        Score = 0;
        Vietzi = 3;
        indexIntrebareCurenta = 0;
        //counter = difficultyRamp;
        panelPauza.SetActive(false);
        panelNegru.SetActive(false);
        Mafiot.enabled = false;
        RaspunsMafiot.enabled = false;
        Bula.enabled = false;
        textEuroiCastigati.enabled = false;
    
    }

    private void AmestecareIntrebari()
    {

        System.Random rng = new System.Random();
        SwitchPrimaIntrebare(rng,1);
        SwitchPrimaIntrebare(rng, 2);
        HaosLength = Haos.Count;
        //Haos.OrderBy(a => rng.Next()).ToList();

        for (int i = 3; i < HaosLength; i++)
        {
            int t = rng.Next(3,HaosLength);
            var temp = Haos[i];
            Haos[i] = Haos[t];
            Haos[t] = temp;

            // Debug.Log("kek" + i);
        }

        Haos.Add(Haos[0]);
    }

    private void SwitchPrimaIntrebare(System.Random rng,int i)
    {
        int t = rng.Next(0, 43);
        var temp = Haos[i];
        Haos[i] = Haos[t];
        Haos[t] = temp;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelNegru.activeSelf== true)
            {
                panelNegru.SetActive(false);
            }
            else
                panelNegru.SetActive(true);
        }
        if (pauza <= 0) UpdateTime();
        else
        {
            pauza -= Time.deltaTime;
            if (pauza <= 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Buttons[i].enabled = true;
                }
                if (isGameOver)
                    ShowGameOverScreen();
                else
                    GetNextQuestion();
            }
        }
        ShowTimp();

    }

    private void UpdateTime()
    {
        timpRamas -= Time.deltaTime;
        timpScurs += Time.deltaTime;
        if (timpRamas <= 0)
        {
            ScadeViata();

            if (GameModes.Hardcore)
            {

            }
            else if (GameModes.TimeAttack)
            {

            }
            else
            {
                timpRamas = timpPentruIntrebare;
            }
            if (isGameOver)
            {
                PlayerPrefs.SetInt("FaraTimp", 1);
                timpRamas = 0;
            }
        }
    }

    public void UserSelect(int numarRasp)
    {
        //Debug.Log(numarRasp);
        PunePauza();
        //numarRasp = k[0];
        if (k[0] == numarRasp)
        {
            RaspunsCorect(numarRasp);
        }
        else
        {
            RaspunsGresit(numarRasp);
        }
    }

    private void RaspunsGresit(int numarRasp)
    {
        Haos[indexIntrebareCurenta].audio.Stop();
        sunetRau.Play();
        Buttons[numarRasp].image.sprite = fundalRosu;
        Buttons[k[0]].image.sprite = fundalVerde;
        ScadeViata();
        PlayerPrefs.SetInt("FaraTimp", 0);
    }

    public void RaspunsCorect(int numarRasp)
    {
        Haos[indexIntrebareCurenta].audio.Stop();
        bineMaaa.Play();
        Buttons[numarRasp].image.sprite = fundalVerde;
        Score++;


        if (dificultyIncreaseScores.Contains(Score))
        {
            CresteDificultate();
        }

        if (Score == 30)
        {
            StergeInterpret();
        }
        //GetNextQuestion();


        GiveEuroi();
    }

    private void GiveEuroi()
    {
        int euroiCastigati;
        if (Score < 10)
            euroiCastigati = 10;
        else if (Score < 25)
            euroiCastigati = 15;
        else if (Score < 50)
            euroiCastigati = 20;
        else if (Score < 75)
            euroiCastigati = 25;
        else if (Score < 100)
            euroiCastigati = 30;
        else
            euroiCastigati = 40;

        euroiCastigati -= 5;

        if (GameModes.Hardcore)
        {
            euroiCastigati += Convert.ToInt32(((6 - timpScurs) + 2) * MultiplierHardCore);
        }
        else if (GameModes.TimeAttack)
        {
            euroiCastigati += 5;
            euroiCastigati += Convert.ToInt32(Math.Max((15 - timpScurs), 0) * MultiplierTimeAttack/2);
        }
        else
        {
            euroiCastigati += Convert.ToInt32(30-timpScurs)/2;
        }
        GetEuroi();
        euroi += euroiCastigati;

        //if (timpRamas > timpPentruIntrebare - 2)
          //  euroi += Convert.ToInt32(euroiCastigati / 4);

        PlayerPrefs.SetInt("euroi", euroi);
        PlayerPrefs.SetInt("castig", PlayerPrefs.GetInt("castig", 0) + euroiCastigati);

        textEuroiCastigati.enabled = true;
        textEuroiCastigati.text = "+" + euroiCastigati.ToString();
    }

    private void PunePauza()
    {
        pauza = timpPentruPauza;
        for (int i = 0; i < 4; i++)
        {
            Buttons[i].enabled = false;
        }
    }

    private void ScadeViata()
    {
        if (GameModes.TimeAttack)
        {
            timpRamas =Math.Max(timpRamas-timeAttackPenalty,0);
            textTimpPierdut.text = (-timeAttackPenalty).ToString();
            textTimpPierdut.enabled = true;
            timeAttackPenalty *= 2;
        }
        else {
            Vietzi--;
            Bani[Vietzi].enabled = false;
        }

        if (Vietzi == 0 || GameModes.Hardcore||(GameModes.TimeAttack&&timpRamas==0))
        {
            
            PunePauza();
            Bani[1].enabled = false;
            sunetRau.Play();
            //ShowGameOverScreen();
            isGameOver = true;
          
            //gameOverScreen();
        }
        else
        {
            //GetNextQuestion();
        }
    }

    void GetNextQuestion()
    {
        // TODO  if(indexIntrebareCurenta)
        textEuroi.text = PlayerPrefs.GetInt("euroi").ToString();
        timpScurs = 0;
        for (int i = 0; i < 4; i++)
        {
            Buttons[i].image.sprite = fundalGalben;
        }
        if (indexIntrebareCurenta+1 < Haos.Count)
        {
            System.Random rng = new System.Random();
            k = new List<int>() { 0, 1, 2, 3 };

            int temp;
            int schimb;

            for (int i = 0; i <= 3; i++)
            {
                temp = rng.Next(0, 4);
                schimb = k[temp];
                k[temp] = k[i];
                k[i] = schimb;
            }

            Haos[indexIntrebareCurenta].audio.Stop();
            //Debug.Log("??????????");

            AmestecaVariante();
            indexIntrebareCurenta++;
            ShowScore();

            if (GameModes.Hardcore)
            {
                timpRamas = 5.5f;
            }
            else if (GameModes.TimeAttack)
            {
                //timpRamas = timpPentruIntrebareTimeAttack;
            }
            else
            {
                timpRamas = timpPentruIntrebare;
            }

            mafiaRosu.SetActive(false);
            skipRosu.SetActive(false);
            fiftyRosu.SetActive(false);
            fundalPauzaRosie.SetActive(false);
        }
        else
        {
            ShowGameWonScreen();
        }
        //buttonPauza.image.sprite = fundalVerde;

        Haos[indexIntrebareCurenta].audio.Play();

        buttonPauza.enabled = true;
        //button5050.image.sprite = fundalVerde;
        button5050.enabled = true;
        ButtonMafia.enabled = true;
        Mafiot.enabled = false;
        RaspunsMafiot.enabled = false;
        Bula.enabled = false;
        textEuroiCastigati.enabled = false;
        textTimpPierdut.enabled = false;
    }

    private void AmestecaVariante()
    {
        System.Random rng = new System.Random();
        HashSet<int> varianteSet = new HashSet<int>();

        Intrebare.text = Haos[indexIntrebareCurenta + 1].Intreb;
        Variante[k[0]].text = Haos[indexIntrebareCurenta + 1].Raspuns;

        varianteSet.Add(indexIntrebareCurenta + 1);
        int ran;

        ran = PickRandom(rng, varianteSet);
        Variante[k[1]].text = Haos[ran].Raspuns;
        varianteSet.Add(ran);

        ran = PickRandom(rng, varianteSet);

        Variante[k[2]].text = Haos[ran].Raspuns;
        varianteSet.Add(ran);

        ran = PickRandom(rng, varianteSet);
        Variante[k[3]].text = Haos[ran].Raspuns;
        //Debug.Log(Haos.Count());
    }

    private int PickRandom(System.Random rng, HashSet<int> varianteSet)
    {
        int ran;
        if (indexIntrebareCurenta+2 >= Haos.Count - 15)
        {
            ran = rng.Next(0, Haos.Count);
            while (varianteSet.Contains(ran))
            {
                ran = rng.Next(0, Haos.Count);
            }
        }
        else
        {
            ran = rng.Next(indexIntrebareCurenta + 2, Haos.Count);
            while (varianteSet.Contains(ran))
            {
                ran = rng.Next(indexIntrebareCurenta + 2, Haos.Count);
            }
        }

        return ran;
    }

    public void PlayAudio()
    {
        // Debug.Log("audio");
        Haos[indexIntrebareCurenta].audio.Play();
    }

    void ShowGameOverScreen()
    {

        AdManager20.ShowInterstitial();
        SalveazaHighScore();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("GameOver");
        //CreierFinal.SendMessage("ScoreFinal", Score);        
    }

    void ShowGameWonScreen()
    {
        //Debug.Log("Da ba trece");
        SalveazaHighScore();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("GameWon");
    }

    void ShowScore()
    {
        ScoreText.text = Convert.ToString(Score);
    }

    void ShowTimp()
    {
        if (timpRamas <= 3)
            TimpText.text = timpRamas.ToString("0.00");
        else if (timpRamas <= 0.00000f)
            TimpText.text = "0.00";
        else
            TimpText.text = Convert.ToString(Convert.ToInt32(timpRamas));
    }

    void SalveazaHighScore()
    {
        if (GameModes.TimeAttack)
        {
            PlayerPrefs.SetInt("ScoreTimeattack", Score);

            HighScore = PlayerPrefs.GetInt("HighScoreTimeattack", 0);
            if (Score > HighScore)
                PlayerPrefs.SetInt("HighScoreTimeattack", Score);
        }
        else if (GameModes.Hardcore)
        {
            PlayerPrefs.SetInt("ScoreHardcore", Score);

            HighScore = PlayerPrefs.GetInt("HighScoreHardcore");

            if (Score > HighScore)
                PlayerPrefs.SetInt("HighScoreHardcore", Score);
        }
        else
        {
            PlayerPrefs.SetInt("Score", Score);

            HighScore = PlayerPrefs.GetInt("HighScore", 0);

            if (Score > HighScore)
                PlayerPrefs.SetInt("HighScore", Score);
        }
    }

    /*
    void SalveazaHighScore()
    {
        //FileStream fileStreamScore= new FileStream("highscore.txt",FileAccess.Write());
        IncarcaHighScore();
        if (Score > HighScore)
        {
            using (StreamWriter fileWriter = File.AppendText("highscore.txt"))
            {
                fileWriter.WriteLine(HighScore);
            }
        }
    }
    public void IncarcaHighScore()
    {
        using (StreamReader fileReader = new StreamReader("highscore.txt"))
        {
            HighScore = fileReader.Read();
        }
    }
    */ // DON'T CLICK WutFace
    void StergeInterpret()
    {
        for (int i = 0; i < HaosLength; i++)
        {
            Haos[i].Raspuns = Haos[i].Raspuns.Substring(Haos[i].Raspuns.IndexOf("-") + 1);
        }
    }

    void CresteDificultate()
    {
        timpPentruIntrebare = Math.Max(5.5f, timpPentruIntrebare - 5); // "simplified" xd
    }

    public void ButtonPause_OnClick()
    {
        AdManager20.ShouldBannerBeOn = true;
        AdManager20.RequestBannner(AdSize.Banner, AdPosition.Bottom);
        GetEuroi();
        if (!HasItem("pauze"))
        {
            if (euroi >= costPause)
                ScadeEuroi(costPause);
            else
                return;
        }
        fundalPauzaRosie.SetActive(true);
        buttonPauza.enabled = false;
        panelPauza.SetActive(true);
        Time.timeScale = 0;
        //panel

        if (PlayerPrefs.GetInt("pauze") > 0)
        {
            numarPauze.text = PlayerPrefs.GetInt("pauze").ToString();

        }
        else
        {
            numarPauze.text = (costPause).ToString();
            ImaginiEuroi[0].enabled = true;
        }

    }

    public void ButtonFiftyFifty_OnClick()
    {
        fiftyRosu.SetActive(true);
        GetEuroi();
        if (!HasItem("juma"))
        {
            if (euroi >= cost5050)
                ScadeEuroi(cost5050);
            else
                return;
        }
        button5050.enabled = false;

        List<int> variante = k.GetRange(1, 3);
        System.Random rng = new System.Random();
        int temp = rng.Next(0, 3);
        Buttons[variante[temp]].enabled = false; //there is no better way :)
        Buttons[variante[temp]].image.sprite = fundalRosu;

        variante.RemoveAt(temp);

        temp = rng.Next(0, 2);
        Buttons[variante[temp]].enabled = false;
        Buttons[variante[temp]].image.sprite = fundalRosu;

        if (PlayerPrefs.GetInt("juma") > 0)
        {
            numar5050.text = PlayerPrefs.GetInt("juma").ToString();

        }
        else
        {
            numar5050.text = (cost5050).ToString();
            ImaginiEuroi[1].enabled = true;
        }

    }

    public void ButtonSkip_OnClick()
    {
        skipRosu.SetActive(true);
        GetEuroi();
        if (!HasItem("Skip"))
        {
            if (euroi >= costSkip)
                ScadeEuroi(costSkip);
            else
                return;
        }
        Haos.Add(Haos[indexIntrebareCurenta]);
        GetNextQuestion();

        if (PlayerPrefs.GetInt("Skip") > 0)
        {
            numarSkip.text = PlayerPrefs.GetInt("Skip").ToString();

        }
        else
        {
            numarSkip.text = (costSkip).ToString();
            ImaginiEuroi[2].enabled = true;
        }

    }

    public void ButtonMafia_OnClick()
    {
        mafiaRosu.SetActive(true);
        GetEuroi();
        if (!HasItem("Mafia"))
        {
            if (euroi >= costMafiot)
                ScadeEuroi(costMafiot);
            else
                return;
        }

        ButtonMafia.enabled = false;

        System.Random rng = new System.Random();
        Mafiot.enabled = true;
        RaspunsMafiot.enabled = true;
        string[] vorbe = new string[] { "Eu cred că varianta corectă este: ", "Hmm...sursele mele îmi spun: ", "O știu pe asta, este: " };
     
        Bula.enabled = true;
        if (rng.Next(0, 10) < 9)
        {
            RaspunsMafiot.text = vorbe[rng.Next(0, 3)];
            RaspunsMafiot.text += Variante[k[0]].text;
        }
        else
        {
            RaspunsMafiot.text = vorbe[rng.Next(0, 2)];
            RaspunsMafiot.text += Variante[k[rng.Next(1, 4)]].text;
        }

        if (PlayerPrefs.GetInt("Mafia") > 0)
        {
            numarMafia.text = PlayerPrefs.GetInt("Mafia").ToString();
        }
        else
        {
            numarMafia.text = (costMafiot).ToString();
            ImaginiEuroi[3].enabled = true;
        }
    }

    public void ButtonResume_OnClick()
    {
        AdManager20.ShouldBannerBeOn = false;
        AdManager20.HideBanner();
        panelPauza.SetActive(false);
        Time.timeScale = 1;

    }

    public void ButtonIesire_Onclick()
    {
        Time.timeScale = 1;
        AdManager20.ShouldBannerBeOn = true;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu");
    }
    public void ButtonInapoi_OnClick()
    {
        panelNegru.SetActive(false);
    }

    public void PanelNegru_OnClick()
    {
        panelNegru.SetActive(false);
    }

    void GetEuroi()
    {
        euroi = PlayerPrefs.GetInt("euroi");
    }

    void SetEuroi(int money)
    {
        PlayerPrefs.SetInt("euroi", money);
    }

    void ScadeEuroi(int money)
    {
        textEuroi.text = (euroi - money).ToString();
        PlayerPrefs.SetInt("euroi", euroi - money);
    }

    bool HasItem(string item)
    {
        if (PlayerPrefs.GetInt(item) > 0)
        {
            PlayerPrefs.SetInt(item, PlayerPrefs.GetInt(item) - 1);
            //Debug.Log(PlayerPrefs.GetInt(item));
            return true;
        }
        return false;
    }
}
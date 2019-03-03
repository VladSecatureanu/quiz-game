using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    // Use this for initialization
    public GameObject Panel;
    public Sprite[] LoadingScreens;
    AsyncOperation operation;
    //float TimpLoad = 4;
    void Start()
    {
        Image img = Panel.GetComponent<Image>();
        int count = PlayerPrefs.GetInt("loadingScreen");

        PlayerPrefs.SetInt("loadingScreen", (count + 1) % 3);

        img.sprite = LoadingScreens[count];
        //SceneManager.UnloadScene(SceneManager.GetActiveScene());

        operation = SceneManager.LoadSceneAsync("StarterScene");
        //operation.allowSceneActivation = false;
        //Invoke("LoadScene", 4f);
    }
    void LoadScene()
    {
        operation.allowSceneActivation=true;
    }
    void Update()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CommandMoneyRain : MonoBehaviour
{

    public static GameObject BanutPrefab;
    public Sprite buttonRosu;
    public Sprite buttonVerde;
    Button[] buttons;
    // Use this for initialization
    void Start()
    {
        buttons = this.GetComponentsInChildren<Button>(true);
        //Debug.Log(buttons.Length);

        foreach (Button b in buttons)
        {
            try
            {
                string text = b.GetComponentInChildren<Text>().text;
                text = text.Substring(text.IndexOf('x') + 1).Trim();
                //Debug.Log(text);

                //if (Convert.ToInt32(text) >= PlayerPrefs.GetInt("euroi"))
                    b.onClick.AddListener(() => { numarValoare(Convert.ToInt32(text)); });


            }
            catch { }
        }
        BanutPrefab = (GameObject)Resources.Load("PloaieDeBani", typeof(GameObject));

        //Banut = GameObject.Find("PloaieDeBani");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var b in buttons)
        {
            try
            {
                string text = b.GetComponentInChildren<Text>().text;
                text = text.Substring(text.IndexOf('x') + 1).Trim();

                if (Convert.ToInt32(text) > PlayerPrefs.GetInt("euroi"))
                {
                    b.image.sprite = buttonRosu;
                    b.enabled = false;
                    
                }
                else
                    b.image.sprite = buttonVerde;

            }
            catch { }
        }

    }

    public static void numarValoare(int valoare)
    {
        //Debug.Log("sadf");
        System.Random rng = new System.Random();

        int pozX;
        int pozY;

        GameObject Fanel = GameObject.Find("Canvas");
        Transform pozitie = Fanel.transform;

        valoare = Math.Min(valoare / 100, 22); // nah my man its good



        while (valoare >= 0)
        {
            pozX = rng.Next(100, Screen.width - 50);
            //Debug.Log(Screen.width);
            pozY = rng.Next(Screen.height + 100, Screen.height + 1000);
            //pozX = Random.Range(-597, 597);
            //pozY = Random.Range(480, 754);
            GameObject Banut = BanutPrefab;
            pozitie.position.Set(pozX, pozY, 0);

            Banut.transform.position = new Vector3(pozX, pozY, 0);

            var Ban = Instantiate(Banut);

            Ban.transform.parent = Fanel.transform;
            Ban.transform.SetAsLastSibling();


            //System.Threading.Thread.Sleep(1000);

            valoare--;
        }

    }
}

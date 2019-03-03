using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Muzica : MonoBehaviour {

    // Use this for initialization

    public static Muzica instance = null;
    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "StarterScene")
            Destroy(this.gameObject);
	}
}

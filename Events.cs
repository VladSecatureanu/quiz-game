using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
public class Events : MonoBehaviour {

    // Use this for initialization
    public Button buttonA;
  
    void Start () {
        //buttonA.GetComponents<GUIText>(text);
        
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Button>().onClick.AddListener(() => { });

    }
    public void onClick()
    {

    }
    /*
    public int index;
    private Button myselfButton;
    
    void Start()
    {
        myselfButton = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            myselfButton.onClick.AddListener(() => actionToMaterial(index));
        }
    }

    void actionToMaterial(int idx)
    {
        Debug.Log("change material to HIT  on material :  " + idx);
    }
    */
}

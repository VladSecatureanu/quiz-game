using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMoney : MonoBehaviour {

    public float fallingSpeed;
    public float destroyTime;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position -= new Vector3(0, fallingSpeed, 0);

	}
}

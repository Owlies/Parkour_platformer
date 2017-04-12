using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInitSetUp : MonoBehaviour {

	public bool activatedByDefault;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(activatedByDefault);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

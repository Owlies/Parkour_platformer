using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void Awake() {
		DontDestroyOnLoad(gameObject);
		if (FindObjectsOfType(GetType()).Length > 1) {
             Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

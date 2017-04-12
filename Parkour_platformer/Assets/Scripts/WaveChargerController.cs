using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChargerController : MonoBehaviour {
	public int waveAmount = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!collider.CompareTag("Player")) {
			return;
		}

		collider.SendMessageUpwards("increaseWaveAmount", waveAmount);
		Destroy(this.gameObject);
	}
}

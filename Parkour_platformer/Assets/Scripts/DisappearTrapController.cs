using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearTrapController : MonoBehaviour {
	public float disappearTime = 3.0f;
	private bool startDisappearCountDown = false;
	// Use this for initialization
	void Start () {
		startDisappearCountDown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (startDisappearCountDown) {
			disappearTime -= Time.deltaTime;
		}

		if (disappearTime <= 0) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (!collider.gameObject.CompareTag("Player")) {
			return;
		}

		startDisappearCountDown = true;
	}
}

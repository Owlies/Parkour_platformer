using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour {
	public int photonId;
	private PhotonWaveEffectsController mEffectController;
	// Use this for initialization
	void Start () {
		mEffectController = GameObject.FindGameObjectWithTag("waveCollider").GetComponent<PhotonWaveEffectsController>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!collider.CompareTag("waveBlocker")) {
			return;
		}
		mEffectController.onPhotonDestroyed(photonId);
		Destroy(gameObject);
	}
}

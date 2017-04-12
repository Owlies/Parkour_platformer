using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTriggerController : MonoBehaviour {
	public GameObject triggerActionHandler;
	public float resetTime = 0.0f;
	private float triggerTimer = 0.0f;
	private MusicManager mMusicManager;
	private Animator mAnimator;
	// Use this for initialization
	void Start () {
		mMusicManager = GameObject.FindGameObjectWithTag("musicManager").GetComponent<MusicManager>();
		mAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (resetTime <= 0) {
			return;
		} 
		if (triggerTimer > resetTime) {
			mAnimator.SetBool("triggerEnabled", false);
		}
		triggerTimer += Time.deltaTime;
	}

	IEnumerator setEnable(bool rEnabled) {
		float statesChangeTimer = 0.0f;
		triggerActionHandler.SetActive(rEnabled);
		while (statesChangeTimer < resetTime) {
			statesChangeTimer += Time.deltaTime;
			yield return null;
		}

		triggerActionHandler.SetActive(!rEnabled);
	}

	void handleWaveAction() {
		mMusicManager.PlayWaveTriggerSound();
		mAnimator.SetBool("triggerEnabled", true);
		triggerTimer = 0.0f;
		if (resetTime > 0) {
			StartCoroutine(setEnable(!triggerActionHandler.activeSelf));
		} else {
			triggerActionHandler.SetActive(!triggerActionHandler.activeSelf);
		}
	}
}

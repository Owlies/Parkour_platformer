using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

	public AudioClip abilitySound;
	public AudioClip respwanSound;
	public AudioClip passLevelSound;
	public AudioClip deathSound;
	public AudioClip jumpSound;
	public AudioClip waveTriggerSound;
	private AudioSource mAudioSource;
	// Use this for initialization
	void Start () {
		 mAudioSource = this.GetComponent<AudioSource>();
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

	public void PlayAbilitySound() {
		mAudioSource.PlayOneShot(abilitySound);
	}

	public void PlayPassLevelSound() {
		mAudioSource.PlayOneShot(passLevelSound);
	}

	public void PlayRespwanSound() {
		mAudioSource.PlayOneShot(respwanSound);
	}

	public void PlayWaveTriggerSound() {
		mAudioSource.PlayOneShot(waveTriggerSound);
	}

	public void PlayDeathSound() {
		mAudioSource.PlayOneShot(deathSound);
	}

	public void PlayJumpSound() {
		mAudioSource.PlayOneShot(jumpSound);
	}
}

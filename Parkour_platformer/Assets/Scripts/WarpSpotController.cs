using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpSpotController : MonoBehaviour {
	private MusicManager mMusicManager;
	private ScreenFaderController screenFader;
	public string nextLevelScene;
	// Use this for initialization
	void Start () {
		mMusicManager = GameObject.FindGameObjectWithTag("musicManager").GetComponent<MusicManager>();
		screenFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFaderController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OnTriggerEnter2D(Collider2D collider) {
		if (!collider.CompareTag("Player")) {
			yield break;
		}
		
		mMusicManager.PlayPassLevelSound();
		yield return StartCoroutine(screenFader.FadeToBlack());
		SceneManager.LoadScene(nextLevelScene);
		yield return StartCoroutine(screenFader.FadeToClear());
	}
}

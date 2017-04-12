using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFaderController : MonoBehaviour {
	private Animator mAnimator;
	private bool isFading = false;
	// Use this for initialization
	void Start () {
		mAnimator = this.GetComponent<Animator>();
	}

	public IEnumerator FadeToClear() {
		isFading = true;
		mAnimator.SetTrigger("fadeOut");

		while (isFading) {
			yield return null;
		}
	}

	public IEnumerator FadeToBlack() {
		isFading = true;
		mAnimator.SetTrigger("fadeIn");

		while (isFading) {
			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AnimationComplete() {
		isFading = false;
	}
}

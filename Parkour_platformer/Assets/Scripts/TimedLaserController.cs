using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLaserController : MonoBehaviour {
	public GameObject laserEffect;
	public float timeInterval = 3.0f;
	private float changeTimer = 0.0f;
	private Animator mAnimator;
	private BoxCollider2D mCollider;
	// Use this for initialization
	void Start () {
		mAnimator = this.GetComponent<Animator>();
		mCollider = this.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (changeTimer < timeInterval) {
			changeTimer += Time.deltaTime;
		} else {
			changeTimer = 0.0f;
			laserEffect.SetActive(!laserEffect.activeSelf);
			mAnimator.SetBool("laserEnabled", laserEffect.activeSelf);
			mCollider.enabled = laserEffect.activeSelf;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!collider.CompareTag("Player")) {
			return;
		}

		collider.SendMessageUpwards("death");
	}
}

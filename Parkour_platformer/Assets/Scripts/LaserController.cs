using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
	public GameObject laserEffect;
	private Animator mAnimator;
	private BoxCollider2D mCollider;
	// Use this for initialization
	void Start () {
		mAnimator = this.GetComponent<Animator>();
		mCollider = this.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mCollider.enabled != laserEffect.activeSelf) {
			mCollider.enabled = laserEffect.activeSelf;
		}
 		mAnimator.SetBool("laserEnabled", laserEffect.activeSelf);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!collider.CompareTag("Player")) {
			return;
		}

		collider.SendMessageUpwards("death");
	}
}

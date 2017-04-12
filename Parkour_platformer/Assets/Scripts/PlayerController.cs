﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	// Components
	private Rigidbody2D mRigidbody;
	private WaveController mWaveController;
	private GameObject mWaveCollider;
	private Animator mAnimator;
	private MusicManager mMusicManager;

	// Movements
	public float mJumpForce = 8f;
	private float distanceToGround;
	public float horizontalSpeed = 2.5f;
	private bool isFalling = false;
	private bool isInAir = false;

	// Ability
	// Used as initial amount
	public int currentWaveAmount = 600;
	private float wavePressedTime = 0;
	public float WaveMidPressTimeThreshold = 0.1f;
	public float WaveLongPressTimeThreshold = 2.0f;
	public float WavePressMaximumTime = 4.0f;
	private bool isCastingWave = false;
	
	// Use this for initialization
	void Start () {
		mRigidbody = this.GetComponent<Rigidbody2D>();
		mWaveCollider = GameObject.FindGameObjectWithTag("waveCollider");
		mWaveController = mWaveCollider.GetComponent<WaveController>();
		isCastingWave = false;
		distanceToGround = this.GetComponent<BoxCollider2D>().bounds.max.y - mRigidbody.transform.position.y;
		mAnimator = this.GetComponent<Animator>();
		//mMusicManager = GameObject.FindGameObjectWithTag("musicManager").GetComponent<MusicManager>();
	}
	
	void FixedUpdate() {
		handlePlayerMove();
		handlePlayerRestart();
		//checkPlayerJumpingAnimation();
		checkDeath();
	}
	// Update is called once per frame
	void Update() {
		handleCastWaves();
	}

	// void checkPlayerJumpingAnimation() {
	// 	bool isOnGround = isGrounded();
	// 	if (!isOnGround && mRigidbody.velocity.y < -0.1f) {
	// 		isFalling = true;
	// 		isInAir = true;
	// 		mAnimator.SetBool("isFalling", isFalling);
	// 	}

	// 	if (isFalling && isOnGround) {
	// 		mAnimator.SetTrigger("touchGround");
	// 		mRigidbody.velocity.Set(mRigidbody.velocity.x, 0);
	// 		isFalling = false;
	// 		isInAir = false;
	// 		mAnimator.SetBool("isFalling", isFalling);
	// 	}
	// }

	void handlePlayerMove() {
		mRigidbody.velocity = new Vector2(horizontalSpeed, mRigidbody.velocity.y);

		handleJump();
	}

	public void handleJump () {
		if (isGrounded() && !isInAir && Input.GetKeyDown(KeyCode.UpArrow)) {
			float jumpForce = mJumpForce * Time.deltaTime;
			mRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			isInAir = true;
		}
	}

	public bool isGrounded() {
		RaycastHit2D hit = Physics2D.Raycast(mRigidbody.transform.position, Vector2.down, distanceToGround + 0.6f);
		if (hit.collider != null && hit.collider.CompareTag("photon")) {
			return false;
		}
		return hit.collider != null;
	}

	private void handlePlayerRestart() {
		if (Input.GetKeyDown(KeyCode.R)) {
			death();
		}
	}
	private bool canCastWave() {
		if (isCastingWave) {
			return false;
		}

		if (currentWaveAmount <= 0) {
			return false;
		}

		if (!mWaveController.canCastWave()) {
			return false;
		}

		return true;
	}
	public void handleCastWaves() {
		if (Input.GetKeyDown(KeyCode.Space) && canCastWave()) {
			isCastingWave = true;
		}

		if (!isCastingWave) {
			return;
		}

		if (Input.GetKey(KeyCode.Space)) {
			wavePressedTime += Time.deltaTime;
		}

		if (Input.GetKeyUp(KeyCode.Space) || wavePressedTime >= WavePressMaximumTime) {
			if (wavePressedTime < WaveMidPressTimeThreshold) {
				mWaveController.castWave(WaveController.WaveType.Short);
			} else if (wavePressedTime < WaveLongPressTimeThreshold) {
				mWaveController.castWave(WaveController.WaveType.Mid);
			} else {
				mWaveController.castWave(WaveController.WaveType.Long);
			}
			mAnimator.SetTrigger("castAbility");
			currentWaveAmount -= 1;
			//mMusicManager.PlayAbilitySound();
			wavePressedTime = 0.0f;
			isCastingWave = false;
		}
	}
	// Deprecated
	public void increaseWaveAmount(int amount) {
		currentWaveAmount += amount;
	}

	private void checkDeath() {
		if (mRigidbody.transform.position.y <= -100) {
			death();
		}
	}

	public void death() {
		enabled = false;
		//mMusicManager.PlayDeathSound();
		StartCoroutine(reloadAfterTime(0.1f));
	}

	 IEnumerator reloadAfterTime(float time) {
		 yield return new WaitForSeconds(time);
     	 Scene loadedLevel = SceneManager.GetActiveScene();
    	 SceneManager.LoadScene (loadedLevel.buildIndex);
		//  mMusicManager.PlayRespwanSound();
	}

	public float waveCastCDLeft() {
		float cd = mWaveController.waveCastCD();
		if (cd < 0) {
			cd = 0;
		}

		return cd;
	}

	public WaveController.WaveType waveTypeMessageForUI() {
		if (wavePressedTime < WaveMidPressTimeThreshold) {
			return WaveController.WaveType.Short;
		} else if (wavePressedTime < WaveLongPressTimeThreshold) {
			return WaveController.WaveType.Mid;
		} else {
			return WaveController.WaveType.Long;
		}
	}
}

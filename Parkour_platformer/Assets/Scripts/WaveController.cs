using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	private GameObject mPlayer;
	private CircleCollider2D mCircleCollider;
	private PhotonWaveEffectsController mWaveEffectController;
	public float waveCastCoolDown = 3.0f;
	private float waveCastTimer;
	public float waveSpeed = 0.5f;
	public enum WaveType {Long, Mid, Short};
	public WaveType mWaveType;
	public float waveMaxRadius = 10.0f;
	public float LongWaveSpeed = 0.5f;
	public float LongWaveMaximumRadius = 1000.0f;
	public float MidWaveSpeed = 0.75f;
	public float MidWaveMaximumRadius = 7.5f;
	public float ShortWaveSpeed = 1.0f;
	public float ShortWaveMaximumRadius = 5.0f;
	private Vector3 castPosition;
	private const float triggerTimeMinum = 0.1f;
	private bool castingWave = false;

	Dictionary<string, float> waveTriggerGuard = new Dictionary<string, float>();

	private float waveInitialRadius = 0.0f;
	// Use this for initialization
	void Start () {
		mPlayer = GameObject.FindGameObjectWithTag("Player");
		mCircleCollider = this.GetComponent<CircleCollider2D>();
		mCircleCollider.radius = waveInitialRadius;
		waveCastTimer = 0.0f;
		mWaveEffectController = this.GetComponent<PhotonWaveEffectsController>();
		mCircleCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (waveCastTimer > 0) {
			waveCastTimer -= Time.deltaTime;
		}
		updateWave();
	}
	public bool canCastWave() {
		return waveCastTimer <= 0;
	}

	public float waveCastCD() {
		return waveCastTimer;
	}
	public void castWave(WaveType waveType) {
		mWaveType = waveType;
		waveCastTimer = waveCastCoolDown;
		if (waveType == WaveType.Long) {
			waveSpeed = LongWaveSpeed;
			waveMaxRadius = LongWaveMaximumRadius;
		} else if (waveType == WaveType.Mid) {
			waveSpeed = MidWaveSpeed;
			waveMaxRadius = MidWaveMaximumRadius;
		} else if (waveType == WaveType.Short) {
			waveSpeed = ShortWaveSpeed;
			waveMaxRadius = ShortWaveMaximumRadius;
		}
		// Debug.Log("waveSpeed: " + waveSpeed + ", waveRadius" + waveMaxRadius);
		mCircleCollider.enabled = true;
		mCircleCollider.radius = waveInitialRadius;
		castPosition = mPlayer.transform.position;
		//StartCoroutine(waveStart(waveType));
		waveStart(waveType);
	}

	void waveStart(WaveType waveType) {
		mWaveEffectController.PlayEffect(waveType);
		castingWave = true;
		
		
	}

	void updateWave() {
		if (castingWave && mCircleCollider.radius < waveMaxRadius) {
			mCircleCollider.radius += waveSpeed * Time.deltaTime;
			mCircleCollider.transform.position = castPosition;
		} else if (castingWave) {
			castingWave = false;
			mCircleCollider.radius = waveInitialRadius;
			mCircleCollider.enabled = false;
		}
	}

	bool canWaveTriggerObject(Vector3 playerPosition, Vector3 targetPosition) {
		float distance = Vector2.Distance(playerPosition, targetPosition);
		RaycastHit2D[] hits = Physics2D.RaycastAll(playerPosition, targetPosition - playerPosition, distance);
		for (int i = 0; i < hits.Length; ++i) {
			if (hits[i].collider != null && hits[i].collider.CompareTag("waveBlocker")) {
				return false;
			}
		}
		
		return true;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!collider.CompareTag("waveTrigger")) {
			return;
		}

		if (!canWaveTriggerObject(castPosition, collider.transform.position)) {
			return;
		}
		float curTime = Time.time;
		if (waveTriggerGuard.ContainsKey(collider.name)) {
			float lastTriggerTime = waveTriggerGuard[collider.name];
			if (curTime - lastTriggerTime < triggerTimeMinum) {
				return;
			}
		}

		waveTriggerGuard[collider.name] = curTime;

		collider.SendMessageUpwards("handleWaveAction");
	}


}

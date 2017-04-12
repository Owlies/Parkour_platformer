using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonWaveEffectsController : MonoBehaviour {
	private const float ShortWaveMultiplier = 6.75f;
	private const float MidWaveMultiplier = 7.5f;
	private const float LongWaveMultiplier = 4.5f;
	public GameObject photonPrefab;
	public int numberOfPhoton = 180;
	public Color waveColor = Color.blue;
	public float photonSpeedShort = 1.0f;
	public float photonSpeedMid = 0.75f;
	public float photonSpeedLong = 0.5f;
	public float photonShortMaximumRange = 5.0f;
	public float photonMidMaximumRange = 7.5f;
	public float photonLongMaximumRange = 10.0f;
	private float photonSpeed = 0.5f;
	private float photonMaximumRange = 10.0f;
	private GameObject[] photons;
	private LineRenderer line;
	private LineRenderer[] lines;
	private int currLines = 0;
	public Material material;
	public float lineWidth = 0.5f;
	private Vector3 createPos;
	private GameObject mPlayer;

	private float destroyTimeLimit = 60.0f;
	private float lastCastTime = 0.0f;
	// Use this for initialization
	void Start () {
		if (photons == null) {
			photons = new GameObject[numberOfPhoton];
		}
 		if (lines == null) {
			lines = new LineRenderer[numberOfPhoton];
		}
		lastCastTime = 0;
		destroyTimeLimit = 60.0f;
		mPlayer = GameObject.FindGameObjectWithTag("Player");
	}

	private void createLine(Vector3 startPos, Vector3 endPos) {
		if (currLines >= numberOfPhoton) {
			return;
		}
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
		lines[currLines++] = line;
        line.material = material;
		line.startColor = waveColor;
		line.endColor = waveColor;
        line.numPositions = 2;
		line.startWidth = lineWidth;
		line.endWidth = lineWidth;
        line.useWorldSpace = true;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
        line.gameObject.layer = 5;
    }

	void Update() {
		updateLines();
		updatePhotons();
		lastCastTime += Time.deltaTime;
		if (destroyTimeLimit < lastCastTime) {
			clearLinesAndPhotons();
		}
	}

	private void updateLines() {
		for (int i = 0; i < numberOfPhoton; ++i) {
			if (photons[i] != null && photons[(i+1) % numberOfPhoton] != null) {
				LineRenderer line = lines[i];
				line.SetPosition(0, photons[i].transform.position);
        		line.SetPosition(1, photons[(i+1) % numberOfPhoton].transform.position);
			}
		}
	}

	private void updatePhotons() {
		for (int i = 0; i < numberOfPhoton; ++i) {
			if (photons[i] != null) {
				if (Vector3.Distance(photons[i].transform.position, createPos) > photonMaximumRange) {
					clearLinesAndPhotons();
				}
				photons[i].transform.Translate(photons[i].transform.right * photonSpeed * Time.deltaTime);
				
			}
		}
	}

	public void onPhotonDestroyed(int index) {
		removeLine(index);
		removeLine((index - 1 + numberOfPhoton) % numberOfPhoton);
	}

	private void removeLine(int index) {
		if (lines[index] != null) {
			Destroy(lines[index].gameObject);
		}
	}
	private void connectAllPhotons() {
		if (lines == null) {
			lines = new LineRenderer[numberOfPhoton];
		}
		for (int i = 0; i < numberOfPhoton; ++i) {
			createLine(photons[i].transform.position, photons[(i+1) % numberOfPhoton].transform.position);
		}
	}

	private void createPhotons() {
		if (photons == null) {
			photons = new GameObject[numberOfPhoton];
		}
		createPos = mPlayer.transform.position;
		createPos.z -= 1;
		Quaternion rotation = new Quaternion();
		float degreeDifference = 360.0f / numberOfPhoton;
		for (int i = 0; i < numberOfPhoton; ++i) {
			GameObject photon = Instantiate(photonPrefab, createPos, rotation);
			photon.GetComponent<PhotonController>().photonId = i;
			rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z + degreeDifference);
			photons[i] = photon;
		}
	}

	public void clearLinesAndPhotons() {
		for (int i = 0; i < numberOfPhoton; ++i) {
			if (lines[i] != null) {
				Destroy(lines[i].gameObject);
			}
        }

		for (int i = 0; i < numberOfPhoton; ++i) {
			if (photons[i] != null) {
				Destroy(photons[i].gameObject);
			}
		}

		currLines = 0;
	}
	public void PlayEffect(WaveController.WaveType waveType) {
		if (waveType == WaveController.WaveType.Long) {
			photonSpeed = photonSpeedLong * LongWaveMultiplier;
			photonMaximumRange = photonLongMaximumRange;
		} else if (waveType == WaveController.WaveType.Mid) {
			photonSpeed = photonSpeedMid * MidWaveMultiplier;
			photonMaximumRange = photonMidMaximumRange;
		} else if (waveType == WaveController.WaveType.Short) {
			photonSpeed = photonSpeedShort * ShortWaveMultiplier;
			photonMaximumRange = photonShortMaximumRange;
		}

		clearLinesAndPhotons();
		createPhotons();
		connectAllPhotons();
		lastCastTime = 0.0f;
	}
}

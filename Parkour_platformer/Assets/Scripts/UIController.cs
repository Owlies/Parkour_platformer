using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public Text playerWaveAmount;
	public Text playerWaveCD;
	public Text playerWaveType;
	public Text dynamicText;
	private PlayerController mPlayerController;
	private string currentScene;

	private Dictionary<string, string> story = new Dictionary<string, string>();
	// Use this for initialization
	void Start () {
		mPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		currentScene = SceneManager.GetActiveScene().name;
		story["Stage_1"] = "Every world has it’s own frequency waves. After the explosion of my solar system, the frequency waves of the whole universe started to lose order, and thousands of planets, solar systems broke up.\n" + 
		"How long has this world be in chaos?\nI don’t know.\nAfter escape from my planet, all I do is passing through different fragmentized world lines, and trying to avoid be drawn into the frequency crack.";
		story["Stage_2"] = "Every time, when I jump to a new world line, my ability to use the waves recharges.\nAs expected, in this world line, the wave frequency is also distorted.\nIs there still any world line survived the explosion?";
		story["Stage_3"] = "I used to have companies, but we lost each other after countless times of world line jumps.\nI’m on my own now, I have to find the correct world line waves, and travel back to my home world, then, I might have a chance to stop the tragedy from happening.";
		story["Stage_4"] = "Danger lies everywhere, I can feel the waves of lasers, I can hear the whispers from those who died here.\nOne wrong step, I will be like them, but I have to get over it, because there is also hope lies in front of me, beyond those dangers.";
		story["Stage_5"] = "How cheerful! My wave has influenced this world line!\n Maybe I can fix the world line, correct the frequency waves!";
		story["Stage_6"] = "I don’t remember this place, but somehow, I feel familiarity.\nWhen I start to correct the frequency waves of this world, my hope has getting stronger and stronger.\nI truly believe I will find the right world line, and save my solar system.";
		story["Stage_7"] = "The collapse of this world line’s frequency wave has been slow down, and more and more frequency has been corrected.\nMaybe there are still someone like me, who is trying to survive, seeking the undamaged world line.";
		story["Stage_8"] = "Fixing this world is just a start, I’ll find the undamaged world line, and travel back to save the people I loved.\nWe must accept finite disappointment, but we must never lose infinite hope.";
		dynamicText.text = story[currentScene];
	}
	
	// Update is called once per frame
	void Update () {
		if (mPlayerController == null) {
			mPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		}
		playerWaveAmount.text = "Waves Left: " + mPlayerController.currentWaveAmount;
		playerWaveCD.text = "Cast CD: " + mPlayerController.waveCastCDLeft();
		WaveController.WaveType waveType = mPlayerController.waveTypeMessageForUI();
		if (waveType == WaveController.WaveType.Short) {
			playerWaveType.text = "Short Wave";
			playerWaveType.color = Color.green;
		} else if (waveType == WaveController.WaveType.Mid) {
			playerWaveType.text = "Mid Wave";
			playerWaveType.color = Color.yellow;
		} else if (waveType == WaveController.WaveType.Long) {
			playerWaveType.text = "Long Wave";
			playerWaveType.color = Color.magenta;
		}

		if (currentScene != SceneManager.GetActiveScene().name) {
			updateDynamicText();
		}
	}

	void updateDynamicText() {
		currentScene = SceneManager.GetActiveScene().name;
		dynamicText.text = story[currentScene];
	}
}

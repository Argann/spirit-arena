﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

	[SerializeField]
	private WaveManager waveManager;

	[SerializeField]
	private GameObject upgradeScreenCanvas;

	[SerializeField]
	private Slider timerUI;

	[SerializeField]
	private Text scoreP1UI;

	[SerializeField]
	private Text scoreP2UI;

	[SerializeField]
	private Image upgradeImage;

	[SerializeField]
	private Text upgradeDescription;

	private int scoreP1;
	private int scoreP2;

	[SerializeField]
	private float minigameTimer;

	[SerializeField]
	private float afterMinigameTimer;

	private float currentTimer;

	private bool firstFrameP1 = true;

	private bool firstFrameP2 = true;

	private int state = -1;

	private int winnerPlayer = 0;

	public bool currentlyUpgrading = false;

	void ApplyUpgrade() {
		// You must apply the upgrade here
		// You can use the `winnerPlayer` var

		// And then we go back to the game
		Debug.Log("END OF UPGRADE");
		upgradeScreenCanvas.SetActive(false);
		waveManager.upgradeComplete = true;
		currentlyUpgrading = false;
		state = -1;
	}

	public void StartUpgrade() {

		currentlyUpgrading = true;

		Debug.Log("Start Upgrade");

		currentTimer = minigameTimer;

		state = 0;

		scoreP1 = 0;
		scoreP2 = 0;

		scoreP1UI.text = "0";
		scoreP2UI.text = "0";

		scoreP1UI.color = new Color(144, 144, 144);
		scoreP2UI.color = new Color(144, 144, 144);

		upgradeScreenCanvas.SetActive(true);
	}


	
	// Update is called once per frame
	void Update () {

		// Minigame State
		if (state == 0) {

			if (currentTimer > 0) {
				currentTimer -= Time.deltaTime;

				timerUI.value = Mathf.InverseLerp(0, 7, currentTimer);

				if (Input.GetAxisRaw("P1_interact") > 0 && firstFrameP1) {
					scoreP1++;
					scoreP1UI.text = ""+scoreP1;
					firstFrameP1 = false;
				}

				if (Input.GetAxisRaw("P1_interact") == 0) {
					firstFrameP1 = true;
				}

				if (Input.GetAxisRaw("P2_interact") > 0 && firstFrameP2) {
					scoreP2++;
					scoreP2UI.text = ""+scoreP2;
					firstFrameP2 = false;
				}

				if (Input.GetAxisRaw("P2_interact") == 0) {
					firstFrameP2 = true;
				}
			} else {
				Debug.Log("Passage au state 1");
				state = 1;
			}


		} else if (state == 1) {
			// Waiting time after minigame

			currentTimer = afterMinigameTimer;

			if (scoreP1 > scoreP2) {
				winnerPlayer = 1;
			} else if (scoreP1 < scoreP2) {
				winnerPlayer = 2;
			} else {
				winnerPlayer = Random.Range(1, 3);
			}

			if (winnerPlayer == 1) {
				scoreP1UI.color = Color.green;
			} else {
				scoreP2UI.color = Color.green;
			}

			Debug.Log("Passage au state 2");
			state = 2;

		} else if (state == 2) {

			if (currentTimer > 0) {
				currentTimer -= Time.deltaTime;
			} else {
				Debug.Log("Passage au state 3");
				state = 3;
			}

		} else if (state == 3) {
			Debug.Log("Current State : "+state);
			ApplyUpgrade();
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

	[SerializeField]
	private PlayerControls[] players;

	[SerializeField]
	private List<Buff> buffs;

	private Buff currentBuff;

	[SerializeField]
	private WaveManager waveManager;

	[SerializeField]
	private GameObject upgradeScreenCanvas;

	[SerializeField]
	private Slider timerUI;

	[SerializeField]
	private Image upgradeImage;

	[SerializeField]
	private Text upgradeDescription;

	[SerializeField]
	private float minigameTimer;

	[SerializeField]
	private float afterMinigameTimer;

	private float currentTimer;

	private int state = -1;

	private PlayerControls winnerPlayer = default;

	public bool currentlyUpgrading = false;

	public bool endgame = false;

	private bool inputEnabled = false;

	private bool[] firstFrame;

	/*
 	 *  Bloc dedie a la gestion d'input avec le New Input System (2019) 
	 *  A conserver pour version future
	 */

	/*
	public void Awake() {
		inputEnabled = false;
		for (int i = 0; i<players.Length; i++) {
			PlayerControls pc = players[i];
			
			var minigameControl = pc.iaa.GetActionMap("gameplay");
			InputAction iaAction = minigameControl.GetAction("action");
			iaAction.performed += ctx=> {
				if (inputEnabled)
					pc.IncreasePlayerCpt(ctx);
			};
		}
	}
	*/

	void ApplyUpgrade() {
		// You must apply the upgrade here
		// You can use the `winnerPlayer` var
		PlayerControls pc = winnerPlayer;
		pc.UpgradeCount += 1;

		if (currentBuff.type == Buff.BuffType.AttackDmg) {

			pc.DamageMultiplicator += currentBuff.mult;

		} else if (currentBuff.type == Buff.BuffType.AttackSpeed) {

			pc.AttackSpeedMultiplicator *= currentBuff.mult;

		} else if (currentBuff.type == Buff.BuffType.MovementSpeed) {

			pc.MovementSpeedMultiplicator += currentBuff.mult;

		} else if (currentBuff.type == Buff.BuffType.BonusTime) {

			pc.BonusDurationMultiplicator += currentBuff.mult;

		} else if (currentBuff.type == Buff.BuffType.Armor) {

			pc.Armor +=  (int) currentBuff.mult;

		} else if (currentBuff.type == Buff.BuffType.MaxHealth) {

			pc.IncreaseMaxHealth((int) currentBuff.mult);
		
		}

		// We reset life of dead heroes
		foreach(PlayerControls player in players) {
			if (player.lifePoints <= 0) {
				player.lifePoints = player.maxLifePoints / 2;
				player.hpSlider.value = player.lifePoints;
				player.gameObject.transform.rotation = new Quaternion(0, 0, 0, player.gameObject.transform.rotation.w);
			}
		}

		// And then we go back to the game
		upgradeScreenCanvas.SetActive(false);
		waveManager.upgradeComplete = true;
		currentlyUpgrading = false;
		state = -1;
	}

	public void StartUpgrade() {
		int totalLife = 0;

		foreach (PlayerControls player in players)
			totalLife += player.lifePoints;

		if(totalLife > 0) {
			currentlyUpgrading = true;
			currentTimer = minigameTimer;
			state = 0;

			ApplyMinigameMalus();

			currentBuff = buffs[Random.Range(0, buffs.Count)];
			upgradeImage.sprite = currentBuff.image;
			upgradeDescription.text = currentBuff.description;
			upgradeScreenCanvas.SetActive(true);
		} else {
			endgame = true;
		}
	}

	public void ApplyMinigameMalus() {
		int minUpgradeCount = 10_000;
		foreach (PlayerControls player in players) {
			if (player.UpgradeCount < minUpgradeCount)
				minUpgradeCount = player.UpgradeCount;
		}
		foreach (PlayerControls pc in players) {
			pc.minigameScore = (minUpgradeCount - pc.UpgradeCount) * 5;
			pc.minigameScoreUI.text = "" + pc.minigameScore;
			pc.minigameScoreUI.color = new Color(144, 144, 144);
		}
	}

	void Start() {
		firstFrame = new bool[players.Length];
		for(int i = 0; i < firstFrame.Length; i++) {
			firstFrame[i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Minigame State
		if (state == 0) {

			if (currentTimer > 0) {
				currentTimer -= Time.deltaTime;
				timerUI.value = Mathf.InverseLerp(0, 7, currentTimer);

				for (int i = 0; i < players.Length; i++) {
					PlayerControls pc = players[i];
					string label = string.Concat(Constants.INTERACT, pc.playerPrefix);

					if (Input.GetAxisRaw(label) > 0 && firstFrame[i]) {
						pc.minigameScore++;
						pc.minigameScoreUI.text = pc.minigameScore.ToString();
						SoundManager.PlaySoundMinigameHit();
						firstFrame[i] = false;
					}

					if (Input.GetAxisRaw(label) == 0) {
						firstFrame[i] = true;
					}
				}
			} else {
				state = 1;
				inputEnabled = false;
			}


		} else if (state == 1) {
			// Waiting time after minigame

			currentTimer = afterMinigameTimer;
			
			winnerPlayer = players[Random.Range(0, players.Length)];

			foreach (PlayerControls pc in players) {
				if(pc.minigameScore > winnerPlayer.minigameScore) {
					winnerPlayer = pc;
				}
			}

			winnerPlayer.minigameScoreUI.color = Color.green;
			state = 2;

		} else if (state == 2) {

			if (currentTimer > 0) {
				currentTimer -= Time.deltaTime;
			} else {
				state = 3;
			}

		} else if (state == 3) {
			ApplyUpgrade();
		}
	}
}

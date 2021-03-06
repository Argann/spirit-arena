﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.SceneManagement;

/*
 * Active l'ecran de game over et permet de relancer la partie
 */
public class GameOverChecker : MonoBehaviour {

	[SerializeField]
	private PlayerControls[] players;

	[SerializeField]
	private Canvas gameOverCanvas;

	private bool inputEnabled = false;


	/*
 	 *  Bloc dedie a la gestion d'input avec le New Input System (2019) 
	 *  A conserver pour version future
	 */

	/*
	public void Awake() {
		foreach (PlayerControls pc in players) {
			var minigameControl = pc.iaa.GetActionMap("gameplay");
			InputAction iaAction = minigameControl.GetAction("replay");
			iaAction.started += Replay;
		}
	}

	private void Replay(InputAction.CallbackContext context) {
		if(inputEnabled) {
			inputEnabled = false;
			SceneManager.LoadScene("PlayScene");
		}
	}
	*/

	void Start() {
		gameOverCanvas.gameObject.SetActive(false);
		inputEnabled = false;
	}
	
	void Update () {
		int lifePoints = 0;
		for(int i=0; i<players.Length; i++) {
			string label = string.Concat(Constants.INTERACT, players[i].playerPrefix);
			if (Input.GetAxisRaw(label) > 0 && inputEnabled) {
				inputEnabled = false;
				players[0].ClearInstances();
				SceneManager.LoadScene("PlayScene");
			}
		}
		
		foreach (PlayerControls player in players) 
			lifePoints += player.lifePoints < 0 ? 0 : player.lifePoints;

		if (players.Length > 0 && lifePoints <= 0) {
			gameOverCanvas.gameObject.SetActive(true);
			inputEnabled = true;
		}
	}
}

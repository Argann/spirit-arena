using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour {

	[SerializeField]
	private PlayerControls pc1;

	[SerializeField]
	private PlayerControls pc2;

	[SerializeField]
	private Canvas gameOverCanvas;

	void Start() {
		gameOverCanvas.gameObject.SetActive(false);
	}
	
	void Update () {
		if (pc1.lifePoints <= 0 && pc2.lifePoints <= 0) {
			gameOverCanvas.gameObject.SetActive(true);

			if (Input.GetAxisRaw("P1_interact") > 0) {
				SceneManager.LoadScene("PlayScene");
			}

			if (Input.GetAxisRaw("P1_swap") > 0) {
				Application.Quit();
				Debug.Log("Goodbye !");
			}
		}
	}
}

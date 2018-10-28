using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private string creditsSceneName;

	[SerializeField]
	private string playSceneName;

	[SerializeField]
	private string mainMenuSceneName;

	[SerializeField]
	private GameObject mainMenuGameObject;

	public void ButtonQuit() {
		Application.Quit();
	}

	public void ButtonCredits() {
		SceneManager.LoadScene(this.creditsSceneName);
	}

	public void ButtonPlay() {
		SceneManager.LoadScene(this.playSceneName);
	}

	public void ButtonStart() {
		WaveManager.StartGame();
		mainMenuGameObject.SetActive(false);
	}

	public void ButtonMainMenu() {
		SceneManager.LoadScene(this.mainMenuSceneName);
	}
}

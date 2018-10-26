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

	public void ButtonQuit() {
		Debug.Log("Goodbye !");
		Application.Quit();
	}

	public void ButtonCredits() {
		Debug.Log("Go To Credits ["+ this.creditsSceneName +"] !");
		SceneManager.LoadScene(this.creditsSceneName);
	}

	public void ButtonPlay() {
		Debug.Log("Go To Play ["+ this.playSceneName +"] !");
		SceneManager.LoadScene(this.playSceneName);
	}

	public void ButtonMainMenu() {
		Debug.Log("Go To Main Menu ["+ this.mainMenuSceneName +"] !");
		SceneManager.LoadScene(this.mainMenuSceneName);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private string creditsSceneName;

	[SerializeField]
	private string IntroSceneName;

	[SerializeField]
	private string mainMenuSceneName;

	[SerializeField]
	private GameObject mainMenuGameObject;

	[SerializeField]
	private GameObject ingameHUD;

	[SerializeField]
	private GameObject creditsHUD;

	[SerializeField]
	private Button PlayButton;
	
	[SerializeField]
	private Button BackButton;

	public void Start() {
		PlayButton.Select();
	}

	public void ButtonQuit() {
		Application.Quit();
	}

	public void ButtonCredits() {
		mainMenuGameObject.SetActive(false);
		creditsHUD.SetActive(true);
		BackButton.Select();
	}

	public void ButtonBack() {
		mainMenuGameObject.SetActive(true);
		creditsHUD.SetActive(false);
		PlayButton.Select();
	}

	public void ButtonIntro() {
		SceneManager.LoadScene(this.IntroSceneName);
	}

	public void ButtonStart() {
		WaveManager.StartGame();
		mainMenuGameObject.SetActive(false);
		ingameHUD.SetActive(true);
		GameObject[] tmp = GameObject.FindGameObjectsWithTag("life");
		foreach (GameObject o in tmp) {
			o.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	public void ButtonMainMenu() {
		SceneManager.LoadScene(this.mainMenuSceneName);
	}
}

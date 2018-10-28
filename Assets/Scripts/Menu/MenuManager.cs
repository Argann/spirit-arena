﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	[SerializeField]
	private GameObject ingameHUD;

	[SerializeField]
	private GameObject creditsHUD;

	[SerializeField]
	private Button PlayButton;
	
	[SerializeField]
	private Button BackButton;

	public GameObject controllerImg;

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

	public void ButtonPlay() {
		SceneManager.LoadScene(this.playSceneName);
	}

	public void ButtonStart() {
		WaveManager.StartGame();
		mainMenuGameObject.SetActive(false);
		ingameHUD.SetActive(true);
		GameObject[] tmp = GameObject.FindGameObjectsWithTag("life");
		foreach (GameObject o in tmp) {
			o.GetComponent<SpriteRenderer>().enabled = true;
		}
		controllerImg.SetActive(false);
	}

	public void ButtonMainMenu() {
		SceneManager.LoadScene(this.mainMenuSceneName);
	}
}

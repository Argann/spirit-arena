using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	[SerializeField]
	private string mainMenuSceneName;

	[SerializeField]
	private Button PlayButton;

	// Use this for initialization
	void Start () {
		PlayButton.Select();
	}

	public void ButtonMainMenu() {
		SceneManager.LoadScene(this.mainMenuSceneName);
	}
}

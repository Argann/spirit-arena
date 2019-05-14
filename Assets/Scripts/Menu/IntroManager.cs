using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	[SerializeField]
	private string mainMenuSceneName = default;

	[SerializeField]
	private Button PlayButton = default;

	// Use this for initialization
	void Start () {
		PlayButton.Select();
		Cursor.visible = false;
	}

	public void ButtonMainMenu() {
		SceneManager.LoadScene(this.mainMenuSceneName);
	}
}

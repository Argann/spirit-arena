using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour {
	private float CMSpeed;
	private bool accelerating;
	private float lowCap;
	private float upCap;

	public float endTime;
	public GameObject mask;
	[SerializeField]
	private string mainMenuSceneName;

	// Use this for initialization
	void Start () {
		CMSpeed = 1;
		accelerating = true;
		lowCap = 4.5f;
		upCap = 50f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > -CMSpeed) {
			lowCap = 0f;
		}
		if (accelerating) {
			if(CMSpeed<upCap) CMSpeed*=1.1f;
		} else {
			if(CMSpeed>lowCap) CMSpeed/=1.06f;
		}
		if (transform.position.x < 0) {
			transform.Translate(new Vector2(CMSpeed/10, 0f));
		}
		if (transform.position.x >= -100) accelerating = false;
		if (Time.timeSinceLevelLoad > endTime) {
			Color col = mask.GetComponent<SpriteRenderer>().color;
			col.a += 0.025f;
			mask.GetComponent<SpriteRenderer>().color = col;
			if (col.a >= 1f) {
				SceneManager.LoadScene(this.mainMenuSceneName);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running_IA : MonoBehaviour {
	public float step = 0.02f;
	private GameObject player;
	private int frameMax = 200;
	private int frameCpt = 200;
	private int decision = 0;
	private Vector3 place;

	// Use this for initialization
	void Start () {
		int index = Random.Range(0, 2);
		player = MobManager.GetPlayer(index);
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCpt == frameMax) {
			frameCpt = 0;
			decision = Random.Range(0,2);
			if (decision == 1) {
				float posX = Random.Range(-9f, 9f);
				float posY = Random.Range(-5f, 5f);
				place = new Vector3(posX, posY, 0f);
				frameCpt += 80;
			}
		}
		frameCpt++;

		if (decision == 0)
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
		else
			transform.position = Vector2.MoveTowards(transform.position, place, step);
	}
}

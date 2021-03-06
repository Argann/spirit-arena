﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral_IA : MonoBehaviour {
	public float step = 2f;
	private GameObject player;

	// Use this for initialization
	void Start () {
		int index = Random.Range(0, 2);
		player = MobManager.GetPlayer(index);
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<PlayerControls>().lifePoints <= 0) {
			player = player.GetComponent<PlayerControls>().otherPlayer;
		}
		float x0 = transform.position.x;
		float y0 = transform.position.y;

		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step/2 * Time.deltaTime);

		float x1 = transform.position.x;
		float y1 = transform.position.y;

		transform.Translate(y0-y1,x1-x0,0f);
	}
}

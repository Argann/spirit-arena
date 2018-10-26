using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_IA : MonoBehaviour {
	public float step = 0.02f;
	private Vector2 player_position;

	// Use this for initialization
	void Start () {
		player_position = MobManager.GetPlayerPosition();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, player_position, step);
	}
}

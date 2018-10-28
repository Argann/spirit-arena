using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_IA : MonoBehaviour {
	public float step = 0.02f;
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
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
	}
}

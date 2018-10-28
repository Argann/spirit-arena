using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigzag_IA : MonoBehaviour {
	public float step = 0.02f;
	public int range = 300;
	private int current = 0;
	private int zigzagStep = 3;
	private GameObject player;

	// Use this for initialization
	void Start () {
		int index = Random.Range(0, 2);
		player = MobManager.GetPlayer(index);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(player.GetComponent<PlayerControls>().lifePoints); 
		if(player.GetComponent<PlayerControls>().lifePoints <= 0) {
			player = player.GetComponent<PlayerControls>().otherPlayer;
		}
		float x0 = transform.position.x;
		float y0 = transform.position.y;

		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step/2);

		float x1 = transform.position.x;
		float y1 = transform.position.y;

		current += zigzagStep;
		if ((current >= range) || (current <= -range)) zigzagStep = -zigzagStep;

		transform.Translate(zigzagStep*(y0-y1),zigzagStep*(x1-x0),0f);
	}
}

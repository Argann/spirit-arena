using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk_IA : MonoBehaviour {
	public float step = 0f;
	private GameObject player;
	private int cooldown = 3;
	private float start = 0f;
	private Vector3 fixedPosition;

	// Use this for initialization
	void Start () {
		int index = Random.Range(0, 2);
		player = MobManager.GetPlayer(index);
		fixedPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - start > cooldown && step == 0f) {
			cooldown = Random.Range(1,3);
			fixedPosition = player.transform.position;
			step = 0.08f;
		}
		
		transform.position = Vector2.MoveTowards(transform.position, fixedPosition, step);

		if (Vector3.Distance(transform.position, fixedPosition) < 0.05f && step > 0f) {
			start = Time.time;
			step = 0f;
		}
	}
}

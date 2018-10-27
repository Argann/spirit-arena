using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDodge_IA : MonoBehaviour {
	public float step = 0.02f;
	private GameObject player;
	private int frameCpt = 0;
	private int maxFrame = 0;
	private int width = 0;

	// Use this for initialization
	void Start () {
		int index = Random.Range(0, 2);
		player = MobManager.GetPlayer(index);
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCpt == maxFrame) {
			width = 2 * Random.Range(-1, 2);
			maxFrame = Random.Range(10, 100);
			frameCpt = 0;
		}
		frameCpt++;

		float x0 = transform.position.x;
		float y0 = transform.position.y;

		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

		float x1 = transform.position.x;
		float y1 = transform.position.y;

		transform.Translate(width*(y0-y1),width*(x1-x0),0f);
	}
}

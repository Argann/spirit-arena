using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] mobs_prefab;
	public float delay = 5;
	private float start_time = 0;
	private float end_time;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		end_time = Time.time;
		if (end_time - start_time > delay) {
			this.Spawn();
			start_time = end_time;
		}
	}

	void Spawn() {
		int index = Random.Range(0, 6);
		Instantiate(mobs_prefab[index]);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject[] mobs_prefab;
	public float delay = 5;
	private float start_time = 0;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - start_time > delay) {
			this.Spawn();
			start_time = Time.time;
		}
	}

	void Spawn() {
		int index = Random.Range(0, 6);
		Instantiate(mobs_prefab[index], transform.position, transform.rotation);
	}
}

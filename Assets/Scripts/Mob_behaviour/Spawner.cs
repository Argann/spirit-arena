using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private float delay = 5;
	private Queue<GameObject> enemyQueue = new Queue<GameObject>();
	private float start_time = 0;

	public void AddToQueue(GameObject enemy) {
		enemyQueue.Enqueue(enemy);
	}

	
	// Update is called once per frame
	void Update () {
		if (Time.time - start_time > delay) {
			this.Spawn();
			start_time = Time.time;
		}
	}

	void Spawn() {
		if (enemyQueue.Count > 0) {
			GameObject enemy = enemyQueue.Dequeue();
			enemy.transform.position = transform.position;
			enemy.SetActive(true);
		}
	}
}

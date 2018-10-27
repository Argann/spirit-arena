using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	[SerializeField]
	private List<Spawner> spawners;

	[SerializeField]
	private List<GameObject> enemies;

	private static List<GameObject> currentEnemies = new List<GameObject>();

	private int waveNumber = 0;

	private bool startingWave = false;


	public static void DeleteEnemy(GameObject enemy) {
		Debug.Log("Destruction d'un ennemi ! Reste " + currentEnemies.Count);
		currentEnemies.Remove(enemy);
	}



	private void StartNewWave() {

		startingWave = true;
		
		waveNumber++;

		int enemyNumber = waveNumber * spawners.Count;

		for (int i = 0; i < enemyNumber; i++)
		{
			int spawnIndex = i % spawners.Count;

			GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, transform.rotation);
			enemy.SetActive(false);
			
			spawners[spawnIndex].AddToQueue(enemy);

			currentEnemies.Add(enemy);
		}

		startingWave = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!startingWave && currentEnemies.Count == 0) {
			Debug.Log("Début de la manche "+(waveNumber+1));
			StartNewWave();
		}
	}
}

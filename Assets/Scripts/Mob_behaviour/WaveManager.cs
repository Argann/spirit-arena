using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	[SerializeField]
	private UpgradeManager upgradeManager;

	[SerializeField]
	private List<Spawner> spawners;

	[SerializeField]
	private List<GameObject> enemies;

	[SerializeField]
	private static List<GameObject> currentEnemies = new List<GameObject>();

	private int waveNumber = 0;

	private static bool gameLaunched = false;

	public bool upgradeComplete = false;

	public static void DeleteEnemy(GameObject enemy) {
		currentEnemies.Remove(enemy);
	}

	public static void StartGame() {
		gameLaunched = true;
	}


	private void SpawnEnemies() {
		int enemyNumber = waveNumber * spawners.Count;

		for (int i = 0; i < enemyNumber; i++)
		{
			int spawnIndex = i % spawners.Count;

			GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, transform.rotation);
			enemy.SetActive(false);
			
			spawners[spawnIndex].AddToQueue(enemy);

			currentEnemies.Add(enemy);
		}
	}
	

	void Update() {
		
		if (gameLaunched && currentEnemies.Count == 0 && !upgradeComplete && !upgradeManager.currentlyUpgrading) {
			waveNumber++;
			if (waveNumber > 1) {
				upgradeManager.StartUpgrade();
			} else {
				upgradeComplete = true;
			}
		}

		if (gameLaunched && currentEnemies.Count == 0 && upgradeComplete) {
			SpawnEnemies();
			upgradeComplete = false;
		}

	}

}

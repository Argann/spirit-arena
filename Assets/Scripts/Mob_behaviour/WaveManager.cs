using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	[SerializeField]
	private Text waveNumberUI;

	[SerializeField]
	private UpgradeManager upgradeManager;

	[SerializeField]
	private List<Spawner> spawners;

	[SerializeField]
	private List<GameObject> enemies;

	[SerializeField]
	private List<GameObject> bossBody;

	[SerializeField]
	private List<GameObject> bossSpirit;



	[SerializeField]
	private GameObject blurp;

	private static GameObject sblurp;

	[SerializeField]
	private static List<GameObject> currentEnemies = null;

	private int waveNumber = 0;
	

	private static bool gameLaunched = false;

	public bool upgradeComplete = false;

	void SetWaveNumber(int nbr) {
		waveNumber = nbr;
		waveNumberUI.text = "" + waveNumber;
	}

	public static void DeleteEnemy(GameObject enemy) {
		currentEnemies.Remove(enemy);
		Instantiate(sblurp, enemy.transform.position, Quaternion.identity);
		Destroy(enemy, 0f);
		if (enemy.tag == "Physical") {
			SoundManager.PlaySoundBodyMonster();
		} else if (enemy.tag == "Spirit") {
			SoundManager.PlaySoundSpiritMonster();
		}
	}

	public static void StartGame() {
		gameLaunched = true;
		currentEnemies = new List<GameObject>();
	}


	private void SpawnEnemies() {
		if (waveNumber % 5 != 0) {
			// Vagues classiques
			int enemyNumber = waveNumber * spawners.Count / 2;

			for (int i = 0; i < enemyNumber; i++)
			{
				int spawnIndex = i % spawners.Count;

				GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, transform.rotation);
				enemy.SetActive(false);
				
				spawners[spawnIndex].AddToQueue(enemy);

				currentEnemies.Add(enemy);
			}
		}
		else {
			// Vagues de boss
			// SOLO DE GUITARE

			if (waveNumber == 5 ) {
				//Boss physique
				GameObject enemy = Instantiate(bossBody[Random.Range(0, 2)], transform.position, transform.rotation);
				enemy.SetActive(false);
				spawners[Random.Range(0, spawners.Count)].AddToQueue(enemy);
				currentEnemies.Add(enemy);
			}
			else if (waveNumber == 10) {
				//Boss spirituel
				GameObject enemy = Instantiate(bossSpirit[Random.Range(0, bossSpirit.Count)], transform.position, transform.rotation);
				enemy.SetActive(false);
				spawners[Random.Range(0, spawners.Count)].AddToQueue(enemy);
				currentEnemies.Add(enemy);
			}
			else if (waveNumber == 15) {
				//Boss spirituel
				GameObject enemy = Instantiate(bossBody[2], transform.position, transform.rotation);
				enemy.SetActive(false);
				spawners[Random.Range(0, spawners.Count)].AddToQueue(enemy);
				currentEnemies.Add(enemy);
			}
			else {
				//Boss multiple
				for (int i=0; i<waveNumber/10; i++) {
					GameObject enemy;
					if (i%2 == 0) enemy = Instantiate(bossBody[Random.Range(0, bossBody.Count)], transform.position, transform.rotation);
					else enemy = Instantiate(bossSpirit[Random.Range(0, bossSpirit.Count)], transform.position, transform.rotation);
					enemy.SetActive(false);
					spawners[Random.Range(0, spawners.Count)].AddToQueue(enemy);
					currentEnemies.Add(enemy);
				}
			}
		}
	}
	
	void Start() {
		sblurp = blurp;
	}

	void Update() {
		
		if (gameLaunched && currentEnemies.Count == 0 && !upgradeComplete && !upgradeManager.currentlyUpgrading) {
			SetWaveNumber(waveNumber + 1);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
	public GameObject[] weapons;
	public long spawnTimerMs = 30000;
	public long spawnTimerDeltaMs = 5000;

	private long lastSpawnMs;
	public long nextSpawnMs;

	void Start() {
		lastSpawnMs = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
		nextSpawnMs = spawnTimerMs + (long)Random.Range(-spawnTimerDeltaMs, spawnTimerDeltaMs);
	}
	
	void Update () {
        long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
		if (now > lastSpawnMs + nextSpawnMs)
		{
			lastSpawnMs = now;
			nextSpawnMs = spawnTimerMs + (long)Random.Range(-spawnTimerDeltaMs, spawnTimerDeltaMs);
			int i = (int)(Random.value * weapons.Length);
			float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(25, Screen.height - 25)).y);
            float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 25, 25)).x);
            Instantiate(weapons[i], new Vector2(spawnX, spawnY), Quaternion.identity);
		}
	}
}


 
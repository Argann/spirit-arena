using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public long cooldownMs = 100;
	public float damages = 1;
	public float projectileSpeed = 10;
	public float projectileTtlMs = 100;
	public float distanceToPLayer = 2f;
	public long bonusTtlMs = 2000;
	public long weaponTtlMs = 0;
	public GameObject weaponVignette = null;
	protected long lastShotTimingMs = 0;
	protected long appearanceTimingMs = 0;
	protected long attachementTimeMs = 0;
	public void fire(GameObject playerObject)
	{
        PlayerControls player = playerObject.GetComponent<PlayerControls>();
		long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        if (now >= player.lastShotTimingMs + (long)(player.AttackSpeedMultiplicator * cooldownMs)) {
            player.lastShotTimingMs = now;
            fireImplementation(playerObject);
        }
	}

	public abstract void fireImplementation(GameObject playerObject);

	public void Attach(GameObject playerObject)
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponent<Collider2D>().enabled = false;
		PlayerControls player = playerObject.GetComponent<PlayerControls>();
		player.lastShotTimingMs = 0;
		player.bonusWeapon = gameObject;
		attachementTimeMs = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
		weaponTtlMs = (long)(weaponTtlMs * player.BonusDurationMultiplicator);
	}

	public int GetTimer()
	{
		return (weaponTtlMs > 0) ? (int)(attachementTimeMs + weaponTtlMs - System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond) / 1000 + 1 : 0;
	}

	void Start()
	{
		appearanceTimingMs = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
	}

	void Update() {
		long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
		if ((bonusTtlMs != 0 && attachementTimeMs == 0 && now > appearanceTimingMs + bonusTtlMs)
		 || (weaponTtlMs != 0 && attachementTimeMs != 0 && now > attachementTimeMs + weaponTtlMs))
		{
			GameObject.Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == "Player")
		{
			Attach(coll.gameObject);
		}
	}

	public void createSingleBullet(GameObject bullet, Vector2 position, Vector2 direction, float damageMultiplicator)
	{
		GameObject instance = GameObject.Instantiate(bullet, new Vector3(position.x, position.y, 0), Quaternion.identity);
		instance.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		instance.GetComponent<Timeout>().ttlMillis = projectileTtlMs;
		instance.GetComponent<Bullet>().damages = damages * damageMultiplicator;
	}

	protected Vector2 RotateVector(Vector2 vector, float angle)
	{
		Vector2 nvec = new Vector2(vector.x, vector.y);
		nvec.Normalize();
		float cos = Mathf.Cos(angle);
		float sin = Mathf.Sin(angle);
		return new Vector2(nvec.x * cos - nvec.y * sin, nvec.x * sin + nvec.y * cos);
	}
}
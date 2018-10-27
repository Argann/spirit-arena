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
	public void fire(GameObject playerObject)
	{
        PlayerControls player = playerObject.GetComponent<PlayerControls>();
		long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        if (now >= player.lastShotTiming + (long)(player.attackSpeedMultiplicator * cooldownMs)) {
            player.lastShotTiming = now;
            fireImplementation(playerObject);
        }
	}

	public abstract void fireImplementation(GameObject playerObject);

	public void createSingleBullet(GameObject bullet, Vector2 position, Vector2 direction)
	{
		GameObject instance = GameObject.Instantiate(bullet, new Vector3(position.x, position.y, 0), Quaternion.identity);
		instance.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		instance.GetComponent<Timeout>().ttlMillis = projectileTtlMs;
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
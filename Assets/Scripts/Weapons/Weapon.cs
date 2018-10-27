using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public long cooldownMs = 100;
	public float damages = 1;
	public float projectileSpeed = 10;
	public float projectileTtlMs = 100;
	protected long lastShotTiming = 0;
	public abstract void fire(GameObject playerObject);

	protected Vector2 RotateVector(Vector2 vector, float angle)
	{
		Vector2 nvec = new Vector2(vector.x, vector.y);
		nvec.Normalize();
		float cos = Mathf.Cos(angle);
		float sin = Mathf.Sin(angle);
		return new Vector2(nvec.x * cos - nvec.y * sin, nvec.x * sin + nvec.y * cos);
	}
}
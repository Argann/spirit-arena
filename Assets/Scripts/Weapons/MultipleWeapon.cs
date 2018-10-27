using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleWeapon : Weapon
{
    public int numberOfBullets = 3;
    public float angleDelta = 0.0872665f;
	public override void fire(GameObject playerObject)
    {
        PlayerControls player = playerObject.GetComponent<PlayerControls>();
        long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        if (now >= lastShotTiming + (long)(player.attackSpeedMultiplicator * cooldownMs)) {
            lastShotTiming = now;
            for (int n = 0; n < (int)(numberOfBullets/2); n++)
            {
                // negative angle delta
                GameObject instance1 = GameObject.Instantiate(player.bullet, playerObject.transform.position, playerObject.transform.rotation);
                instance1.GetComponent<Rigidbody2D>().velocity = RotateVector(player.GetAim(), -n * angleDelta) * projectileSpeed;
                instance1.GetComponent<Timeout>().ttlMillis = projectileTtlMs;
                // positive angle delta
                GameObject instance2 = GameObject.Instantiate(player.bullet, playerObject.transform.position, playerObject.transform.rotation);
                instance2.GetComponent<Rigidbody2D>().velocity = RotateVector(player.GetAim(), n * angleDelta) * projectileSpeed;
                instance2.GetComponent<Timeout>().ttlMillis = projectileTtlMs;
            }
            if (numberOfBullets % 2 == 1)
            {
                // front
                GameObject instance = GameObject.Instantiate(player.bullet, playerObject.transform.position, playerObject.transform.rotation);
                instance.GetComponent<Rigidbody2D>().velocity = player.GetAim() * projectileSpeed;
                instance.GetComponent<Timeout>().ttlMillis = projectileTtlMs;
            }
        }
    }
}
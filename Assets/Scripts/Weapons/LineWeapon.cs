using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWeapon : Weapon
{
	public override void fire(GameObject playerObject)
    {
        PlayerControls player = playerObject.GetComponent<PlayerControls>();
        long now = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        if (now >= lastShotTiming + (long)(player.attackSpeedMultiplicator * cooldownMs)) {
            lastShotTiming = now;
            GameObject instance = GameObject.Instantiate(player.bullet, playerObject.transform.position, playerObject.transform.rotation);
			instance.GetComponent<Rigidbody2D>().velocity = player.GetAim() * projectileSpeed;
            instance.GetComponent<Timeout>().ttlMillis = projectileTtlMs;
        }
    }
}
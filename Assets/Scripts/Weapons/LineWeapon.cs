using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWeapon : Weapon
{
	public override void fireImplementation(GameObject playerObject)
    {
        PlayerControls player = playerObject.GetComponent<PlayerControls>();
        Vector2 position = playerObject.transform.position;
        Vector2 direction = player.GetAim();
        float damageMultiplicator = player.DamageMultiplicator;
        createSingleBullet(player.bullet, position + direction * distanceToPLayer, direction, damageMultiplicator);
    }
}
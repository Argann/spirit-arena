using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleWeapon : Weapon
{
    public int numberOfBullets = 5;
    public float angleDelta = 0.2f;

	public override void fireImplementation(GameObject playerObject)
    {
        PlayerControls player = playerObject.GetComponent<PlayerControls>();
        Vector2 position = playerObject.transform.position;
        Vector2 direction = player.GetAim();
        float damageMultiplicator = player.DamageMultiplicator;
        if (numberOfBullets % 2 == 1)
            createSingleBullet(player.bullet, position + direction * distanceToPLayer, direction, player);
        for (int n = 0; n < (int)(numberOfBullets/2); n++)
        {
            Vector2 direction1 = this.RotateVector(direction,  n * angleDelta);
            createSingleBullet(player.bullet, position + direction1 * distanceToPLayer, direction1, player);
            Vector2 direction2 = this.RotateVector(direction, -n * angleDelta);
            createSingleBullet(player.bullet, position + direction2 * distanceToPLayer, direction2, player);
        }
    }
}
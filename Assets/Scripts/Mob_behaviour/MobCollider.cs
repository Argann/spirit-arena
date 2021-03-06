﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {
	public float lifePoints = 10;
	public int damage = 1;
	public bool isBoss = false;
	public bool isDead = false;
	public bool isIntro = false;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag(gameObject.tag)) {
			coll.gameObject.GetComponent<Animator>().SetBool("Exploded", true);
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.StopMe();
			float dealtDamages = Mathf.Min(lifePoints, bullet.damages);
			bullet.player.Points = bullet.player.Points + (int)(dealtDamages * 100);
			if (isIntro) dealtDamages = 1f;
			lifePoints -= dealtDamages;
			if (!isIntro) SoundManager.PlaySoundHit();
			if (lifePoints <= 0)
			{
				if (!isIntro) WaveManager.DeleteEnemy(gameObject);
				GetComponent<Animator>().SetBool("Dead", true);
				damage = 0;
				isDead = true;
			}
		}
		else if (coll.GetComponent<Collider2D>().tag == "Player" && !isDead)
		{
			PlayerControls player = coll.GetComponent<PlayerControls>();
			player.TakeDamages(damage);
			if (isBoss) {
				player.Recoil(5*new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0f));
			}
			else {
				if (!isIntro) WaveManager.DeleteEnemy(gameObject);
				GetComponent<Animator>().SetBool("Dead", true);
				damage = 0;
			}
		}
	}

	public void DestroyMe() {
		Destroy(gameObject, 0f);
	}
}

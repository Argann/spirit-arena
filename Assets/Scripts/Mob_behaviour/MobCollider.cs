using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {
	public float lifePoints = 10;
	public int damage = 1;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == gameObject.GetComponent<Collider2D>().tag) {
			coll.gameObject.GetComponent<Animator>().SetBool("Exploded", true);
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.StopMe();
			lifePoints -= bullet.damages;
			if (lifePoints <= 0)
			{
				WaveManager.DeleteEnemy(gameObject);
				GetComponent<Animator>().SetBool("Dead", true);
			}
		}
		else if (coll.GetComponent<Collider2D>().tag == "Player")
		{
			PlayerControls player = coll.GetComponent<PlayerControls>();
			player.TakeDamages(damage);
			WaveManager.DeleteEnemy(gameObject);
			GetComponent<Animator>().SetBool("Dead", true);
			damage = 0;
		}
	}

	public void DestroyMe() {
		Destroy(gameObject, 0f);
	}
}

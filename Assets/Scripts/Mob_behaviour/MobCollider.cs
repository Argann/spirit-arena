using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {
	public float lifePoints = 10;
	public int damage = 1;
	public bool isDead = false;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == gameObject.GetComponent<Collider2D>().tag) {
			coll.gameObject.GetComponent<Animator>().SetBool("Exploded", true);
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.StopMe();
			float dealtDamages = Mathf.Min(lifePoints, bullet.damages);
			bullet.player.Points = bullet.player.Points + (int)(dealtDamages * 100);
			lifePoints -= dealtDamages;
			if (lifePoints <= 0)
			{
				WaveManager.DeleteEnemy(gameObject);
				GetComponent<Animator>().SetBool("Dead", true);
				isDead = true;
			}
		}
		else if (coll.GetComponent<Collider2D>().tag == "Player" && !isDead)
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

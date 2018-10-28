using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {
	public int lifePoints = 1;
	public int damage = 1;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == gameObject.GetComponent<Collider2D>().tag) {
			Destroy(coll.gameObject, 0f);
			WaveManager.DeleteEnemy(gameObject);
			Destroy(gameObject, 0f);
		}
		else if (coll.GetComponent<Collider2D>().tag == "Player")
		{
			PlayerControls player = coll.GetComponent<PlayerControls>();
			player.TakeDamages(damage);
			WaveManager.DeleteEnemy(gameObject);
			Destroy(gameObject, 0f);
		}
	}
}

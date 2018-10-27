using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {
	public float lifePoints = 10;
	public int damage = 1;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == gameObject.GetComponent<Collider2D>().tag) {
			coll.gameObject.GetComponent<Animator>().SetBool("Exploded", true);
			Debug.Log("life = " + lifePoints);
			Bullet bullet = coll.gameObject.GetComponent<Bullet>();
			bullet.StopMe();
			lifePoints -= bullet.damages;
			if (lifePoints <= 0)
			{
				Debug.Log("life = " + lifePoints);
				Debug.Log("MOOOOOOOOOOOOOOOOOOOOOOOOOOOORT");
				WaveManager.DeleteEnemy(gameObject);
				Destroy(gameObject, 0f);
			}
		}
		else if (coll.GetComponent<Collider2D>().tag == "Player")
		{
			Debug.Log("There is damage");
			PlayerControls player = coll.GetComponent<PlayerControls>();
			player.TakeDamages(damage);
			WaveManager.DeleteEnemy(gameObject);
			Destroy(gameObject, 0f);
		}
	}
}

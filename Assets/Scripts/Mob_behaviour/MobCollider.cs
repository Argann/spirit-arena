using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == gameObject.GetComponent<Collider2D>().tag) {
			Destroy(coll.gameObject, 0f);
			WaveManager.DeleteEnemy(gameObject);
			Destroy(gameObject, 0f);
		}
	}
}

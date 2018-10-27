using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("coucou");
		Destroy(gameObject, 0f);
	}
}

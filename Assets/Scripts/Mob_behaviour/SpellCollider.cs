using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("byebye");
		Destroy(gameObject, 0f);
	}
}

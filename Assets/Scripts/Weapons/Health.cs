using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int lifeAdded = 10;

    void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<Collider2D>().tag == "Player") {
            coll.GetComponent<PlayerControls>().Heal(lifeAdded);
            Destroy(gameObject);
		}
	}
}

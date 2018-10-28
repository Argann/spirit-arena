using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVignette : MonoBehaviour {
	public GameObject playerObject = null;
	private Weapon previous = null;
	private PlayerControls player = null;

	void Start() {
		if (playerObject) player = playerObject.GetComponent<PlayerControls>();
	}
	
	void Update () {
		Weapon current = player.bonusWeapon ? player.bonusWeapon.GetComponent<Weapon>() : player.defaultWeapon.GetComponent<Weapon>();
		if (current != previous)
		{
			previous = current;
			foreach (Transform child in transform) {
				GameObject.Destroy(child.gameObject);
			}
			Instantiate(current.weaponVignette, transform);
		}
	}
}

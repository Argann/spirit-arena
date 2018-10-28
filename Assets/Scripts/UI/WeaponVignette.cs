using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponVignette : MonoBehaviour {
	public GameObject playerObject = null;
	private Weapon previous = null;
	private PlayerControls player = null;

	void Start() {
		if (playerObject) player = playerObject.GetComponent<PlayerControls>();
	}
	
	void Update () {
		Weapon current = player.bonusWeapon ? player.bonusWeapon.GetComponent<Weapon>() : player.defaultWeapon.GetComponent<Weapon>();
		Text cooldown = transform.Find("cooldown").GetComponent<Text>();
		if (cooldown) cooldown.text = (current.GetTimer() > 0) ? current.GetTimer().ToString() : "";
		if (current != previous)
		{
			previous = current;
			foreach (Transform child in transform) {
				if (child.name != "cooldown") GameObject.Destroy(child.gameObject);
			}
			Instantiate(current.weaponVignette, transform);
		}
	}
}

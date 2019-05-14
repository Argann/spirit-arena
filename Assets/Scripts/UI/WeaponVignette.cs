using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponVignette : MonoBehaviour {
	public GameObject playerObject = null;
	private Weapon previous = null;
	private PlayerControls player = null;
	private GameObject instance = null;

	void Start() {
		if (playerObject) player = playerObject.GetComponent<PlayerControls>();
	}
	
	void Update () {
		Weapon current = player.bonusWeapon ? player.bonusWeapon.GetComponent<Weapon>() : player.defaultWeapon.GetComponent<Weapon>();
		if (current)
		{
			Image timerBar = gameObject.GetComponent<Image>();
			if (timerBar)
			{
				timerBar.fillAmount = current.GetTimerProgress();
			}
			if (current != previous)
			{
				previous = current;
				foreach (Transform child in transform) {
					if (child.name != "Timer") GameObject.Destroy(child.gameObject);
				}
				instance = Instantiate(current.weaponVignette, transform);
				instance.transform.SetAsFirstSibling();
			}
		}
		else
		{
			Debug.LogError("no weapon found");
		}
	}
}

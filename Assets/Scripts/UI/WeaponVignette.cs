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
		if (current)
		{
			Transform timer = transform.Find("Timer");
			if (timer)
			{
				Transform circle = timer.Find("Circle");
				if (circle)
				{
					Image timerBar = circle.gameObject.GetComponent<Image>();
					if (timerBar)
					{
						timerBar.fillAmount = current.GetTimerProgress();
					}
				}
			}
			if (current != previous)
			{
				previous = current;
				foreach (Transform child in transform) {
					if (child.name != "Timer") GameObject.Destroy(child.gameObject);
				}
				Instantiate(current.weaponVignette, transform).transform.SetAsFirstSibling();
			}
		}
		else
		{
			Debug.LogError("no weapon found");
		}
	}
}

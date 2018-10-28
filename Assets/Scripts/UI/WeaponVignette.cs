using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponVignette : MonoBehaviour {
	public GameObject playerObject = null;
	private Weapon previous = null;
	private PlayerControls player = null;
	private bool init = true;
	private bool previousSpriritual = false;
	private GameObject instance = null;

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
				instance = Instantiate(current.weaponVignette, transform);
				instance.transform.SetAsFirstSibling();
			}
		}
		else
		{
			Debug.LogError("no weapon found");
		}
		if (init || (instance && previousSpriritual != player.IsSpirit))
		{
			init = false;
			instance.GetComponent<Image>().color = player.IsSpirit ? new Color(0, 156, 226) : new Color(255, 45, 0);
			previousSpriritual = player.IsSpirit;
		}
	}
}

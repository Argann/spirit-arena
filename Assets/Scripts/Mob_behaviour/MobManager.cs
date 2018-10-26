using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour {
	public GameObject player;
	private static GameObject staticPlayer;

	// Use this for initialization
	void Start () {
		staticPlayer = player;
	}

	public static Vector2 GetPlayerPosition() {
		return staticPlayer.transform.position;
	}
}

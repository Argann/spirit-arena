using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour {
	public GameObject[] players;
	private static GameObject[] staticPlayers;

	// Use this for initialization
	void Awake () {
		staticPlayers = players;
	}

	public static GameObject GetPlayer(int i) {
		return staticPlayers[i];
	}
}

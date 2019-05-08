using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeout : MonoBehaviour {
	public float ttlMillis;
	private long startTime;

	void Start () {
		ttlMillis = 0;
	
	void Update () {
		if (ttlMillis > 0)
			Object.Destroy(this.gameObject, ttlMillis);
		}
	}
}
